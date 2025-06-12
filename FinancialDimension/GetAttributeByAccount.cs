public class DemoGetAttributeByAccount
{
    /// <summary>
    ///    Finds a system-generated dimension attribute for the specified account type.
    /// </summary>
    /// <param name="_accountType">
    ///    The account type for which to retrieve a dimension attribute.
    /// </param>
    /// <param name="_enumType">
    ///    The type of the enumeration that specifies the account type.
    /// </param>
    /// <param name="_custVend">
    ///    A value the indicates whether this is a customer or vendor module.
    /// </param>
    /// <returns>
    ///    A system-generated dimension attribute for the specified account type.
    /// </returns>
    public static DimensionAttribute getAttributeByAccountType(int _accountType, 
															   EnumId _enumType = enumNum(LedgerJournalACType),
															   ModuleInventCustVend _custVend = ModuleInventCustVend::Cust)
    {
        DimensionHierarchyType dimHierarchyType;

        //Set appropriate backing entity information based on type
        dimHierarchyType = DimensionHierarchy::getHierarchyTypeByAccountType(_accountType, _enumType, _custVend);
        return DimensionAttribute::getAttributeByHierarchyType(dimHierarchyType);
    }
}