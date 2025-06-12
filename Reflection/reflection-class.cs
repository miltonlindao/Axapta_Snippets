/*
Considerations:
- In order to access PRIVATE properties of a class through an extension, we are going to rely on a reflection technique

References:
https://shootax.blogspot.com/2018/08/x-d365fo-reflection-getfield-not-return.html
https://ievgensaxblog.wordpress.com/tag/reflection/
https://daxonline.org/1579-how-to-access-call-private-protected-methods-variables-in-a-class-table-using-reflection.html
*/


using System.Reflection; //Must have

[ExtensionOf(classstr(AvailabilityView))]
final class AvailabilityViewDemo_Extension
{
    public str selectChartItem(str _value)
    {
        str ret = next selectChartItem(_value);

        Info('selectChartItem');
        
		//All flags are enabled to avoid compatibility issues during testing. In case of dev for delivery, only leave the necessary ones
        var bindFlags = BindingFlags::Instance |
                        BindingFlags::NonPublic |
                        BindingFlags::Static |
                        BindingFlags::Public;
        var field = this.GetType().GetField(identifierStr(chartItemIdSelect), bindFlags); //Instead of using identifierStr you can also put a text string with the name of the variable/property

        if (field)
        {
            FormProperty chartItemIdSelect_reflected = field.GetValue(this);
            Info('selectChartItem: ' + chartItemIdSelect_reflected.parmValue());
        }
        else
        {
            Info('Cannot access chartItemIdSelect in selectChartItem');
        }
        
        return ret;
    }

    public str deselectChartItem(str _value)
    {
        str ret = next deselectChartItem(_value);

        Info('deselectChartItem');

        var bindFlags = BindingFlags::Instance |
                        BindingFlags::NonPublic |
                        BindingFlags::Static |
                        BindingFlags::Public;
        var field = this.GetType().GetField(identifierStr(chartItemIdDeselect), bindFlags);
        
        if (field)
        {
            FormProperty chartItemIdDeselect_reflected = field.GetValue(this);
            Info('deselectChartItem: ' + chartItemIdDeselect_reflected.parmValue());
        }
        else
        {
            Info('Cannot access chartItemIdSelect in selectChartItem');
        }
        
        return ret;
    }

    public str selectTimePeriod(str _value)
    {
        str ret = next selectTimePeriod(_value);

        Info('selectTimePeriod');

        var bindFlags = BindingFlags::Instance |
                        BindingFlags::NonPublic |
                        BindingFlags::Static |
                        BindingFlags::Public;
        var field = this.GetType().GetField(identifierStr(timelineKeySelect), bindFlags);
        
        if (field)
        {
            FormProperty timelineKeySelect_reflected = field.GetValue(this);
            Info('selectTimePeriod: ' + timelineKeySelect_reflected.parmValue());
        }
        else
        {
            Info('Cannot access timelineKeySelect in selectTimePeriod');
        }
        
        return ret;
    }

    public str deselectTimePeriod(str _value)
    {
        str ret = next deselectTimePeriod(_value);

        Info('deselectTimePeriod');

        var bindFlags = BindingFlags::Instance |
                        BindingFlags::NonPublic |
                        BindingFlags::Static |
                        BindingFlags::Public;
        var field = this.GetType().GetField(identifierStr(timelineKeyDeselect), bindFlags);
        
        if (field)
        {
            FormProperty timelineKeyDeselect_reflected = field.GetValue(this);
            Info('deselectTimePeriod: ' + timelineKeyDeselect_reflected.parmValue());
        }
        else
        {
            Info('Cannot access timelineKeyDeselect in deselectTimePeriod');
        }
        
        return ret;
    }

    public str selectCollectionItem(str _value)
    {
        str ret = next selectCollectionItem(_value);

        Info('selectCollectionItem');

        var bindFlags = BindingFlags::Instance |
                        BindingFlags::NonPublic |
                        BindingFlags::Static |
                        BindingFlags::Public;
        var field = this.GetType().GetField(identifierStr(collectionKeySelect), bindFlags);
        
        if (field)
        {
            FormProperty collectionKeySelect_reflected = field.GetValue(this);
            Info('selectCollectionItem: ' + collectionKeySelect_reflected.parmValue());
        }
        else
        {
            Info('Cannot access collectionKeySelect in selectCollectionItem');
        }
        
        return ret;
    }

    public str deselectCollectionItem(str _value)
    {
        str ret = next deselectCollectionItem(_value);

        Info('deselectCollectionItem');

        var bindFlags = BindingFlags::Instance |
                        BindingFlags::NonPublic |
                        BindingFlags::Static |
                        BindingFlags::Public;
        var field = this.GetType().GetField(identifierStr(collectionKeyDeselect), bindFlags);
        
        if (field)
        {
            FormProperty collectionKeyDeselect_reflected = field.GetValue(this);
            Info('deselectCollectionItem: ' + collectionKeyDeselect_reflected.parmValue());
        }
        else
        {
            Info('Cannot access collectionKeyDeselect on deselectCollectionItem');
        }
        
        return ret;
    }

    protected boolean registerItemSelection(str _itemType, str _id)
    {
        boolean ret = next registerItemSelection(_itemType, _id);

        Info('registerItemSelection');

        return ret;
    }

    protected boolean registerItemDeselection(str _itemType, str _id)
    {
        boolean ret = next registerItemDeselection(_itemType, _id);

        Info('registerItemDeselection');

        return ret;
    }

}
