public class CreateWorkerFinancialDimensions
{
    public void createOrUpdDerivedDimension(HcmEmployment _workerEmployment, str _dimensionAttributeName)
    {
        DimensionAttributeValueSetStorage			attributeValueStorage;
        DimensionAttributeValueSetStorage			attrNewValueStorage;
        DimensionAttribute							dimensionAttribute;
        DimensionAttributeValueDerivedDimensions 	derivedDimensions;
        DimensionHierarchy 							dimensionHierarchy;
        HcmWorker 									worker;
        Map 										dimensionMap;
        MapIterator 								mapIterator;
        RefRecId 									workerDimRecId;
        RefRecId 									costcenterDimRecId;
        RefRecId 									departmentDimRecId;
        RefRecId 									sectorDimRecId;
        RefRecId 									businessUnitDimRecId;
        Counter 									loop;

        worker = HcmWorker::find(_workerEmployment.Worker);

        dimensionMap            = new Map(Types::Integer, Types::Int64);
        attrNewValueStorage     = new DimensionAttributeValueSetStorage();
        attributeValueStorage   = DimensionAttributeValueSetStorage::find(_workerEmployment.DefaultDimension);
        dimensionAttribute      = DimensionAttribute::findByName(_dimensionAttributeName);
        dimensionHierarchy      = DimensionAttributeDerivedDimensions::findOrCreateDrivingDimension(dimensionAttribute);
 
        for (loop = 1; loop <= DimensionDerivationsFormHelper::MaxDerivedSegments + 1; loop++)
        {
            DimensionHierarchyLevel dimHierarchyLevel = DimensionHierarchyLevel::findByDimensionHierarchyAndLevel(dimensionHierarchy.RecId, loop);
 
            if (dimHierarchyLevel)
            {
                DimensionAttributeDerivedDimensions derivedDimension = DimensionAttributeDerivedDimensions::findByDimensionHierarchyLevel(dimHierarchyLevel);
 
                if (derivedDimension)
                {
                    DimensionAttribute dimAttr = DimensionAttribute::find(dimHierarchyLevel.DimensionAttribute);
 
                    dimensionMap.insert(derivedDimension.DerivedDimensionFieldNum, dimAttr.RecId);
                }
            }
        }
 
        mapIterator = new MapIterator(dimensionMap);
 
        RefRecId workerAttributeValueRecId = DimensionAttributeValue::findByDimensionAttributeAndValue(dimensionAttribute, worker.PersonnelNumber, false, false).RecId;

        if (!workerAttributeValueRecId)
            return;

        derivedDimensions = DimensionAttributeValueDerivedDimensions::findByDimensionAttributeValue(dimensionAttribute.RecId, workerAttributeValueRecId);

        if (!derivedDimensions)
        {
            //If not derived dimensions value found
            derivedDimensions.DimensionAttribute        = dimensionAttribute.RecId;
            derivedDimensions.DimensionAttributeValue   = workerAttributeValueRecId;
        }
 
        derivedDimensions.DefaultDimension          = attrNewValueStorage.save();
        while (mapIterator.more())
        {
            DimensionAttribute dimValueLocal = DimensionAttribute::find(mapIterator.value());
            //The mapIterator value would be stored at mapIterator.value()

            if (dimValueLocal.Name != _dimensionAttributeName)
            {
                derivedDimensions.(mapIterator.key()) = attributeValueStorage.getValueByDimensionAttribute(dimValueLocal.RecId);
            }

            mapIterator.next();
        }
        
        if (!derivedDimensions.RecId)
        {
            derivedDimensions.insert();
            Info('The related derived dimension was generated. See under financial dimensions');
            //At this point the derivedDimensions were inserted succesfully
        }
        else
        {
            ttsbegin; 
            derivedDimensions.selectForUpdate(true);
            derivedDimensions.update();
            ttscommit;
            Info('The related derived dimension has been updated. See under financial dimensions');
        }
    }
}