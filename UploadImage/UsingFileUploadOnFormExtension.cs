/*
This is a demo class to show how to get an image from user and show it on a formcontrol
*/
[ExtensionOf(formstr(CustTable))]
final class CustTableDemoForm_Extension
{
    private container	imageContainer;
    private str			imageFilePathName;

    public str parmimageFilePathName(str _imageFilePathName = imageFilePathName)
    {
        this.imageFilePathName = _imageFilePathName;
        return this.imageFilePathName;
    }
	
    public container parmimageContainer(container _imageContainer = imageContainer)
    {
        this.imageContainer = _imageContainer;
        return this.imageContainer;
    }

    public void DemoCustLogo_Delete()
    {
        FormWindowControl LogoImage = this.design().controlName(formControlStr(CustTable, LogoImage)) as FormWindowControl;
        ttsbegin;
    
        if (CustTable.RecId)
        {
            //Assign image and save
            CustTable.DemoLogo = conNull();
            CustTable_ds.write() ;
        }
    
        ttscommit;
        CustTable_ds.reread();
        CustTable_ds.research(true);
        imageContainer = conNull();
        imageFilePathName = '';
        this.showImage(LogoImage);
       
    }

    void DemoCustLogo_insert()
    {
        ttsbegin;
    
        //Assign image and save
        CustTable.DemoLogo = imageContainer;
        CustTable_ds.write() ;
    
        ttscommit;
        CustTable_ds.reread();
        CustTable_ds.research(true);
    }

    boolean uploadImageFile()
    {
        FormRun visualForm;
        FileUpload fileUploadControl;

        str strategyFileFilter = 'ImageFileUploadTemporaryStorageStrategy';
        visualForm = classFactory::formRunClassOnClient(new Args(formstr(SysGetFileFromUser)));
        visualForm.init();
        visualForm.design().caption("@ApplicationPlatform:GetFileImageCaption");
            
        fileUploadControl = visualForm.design().controlName('FileUpload1');
        fileUploadControl.fileTypesAccepted(".bmp,.jpg,.png,.gif");
        visualForm.run();
        visualForm.wait();

        FileUploadTemporaryStorageResult fileUploadResult = fileUploadControl.getFileUploadResult();

        if (fileUploadResult != null && fileUploadResult.getUploadStatus())
        {
            imageFilePathName = fileUploadResult.getDownloadUrl();
            return true;
        }

        return false;
    }

    public void showImage(FormWindowControl _imageControl)
    {
        Image _image;
        try
        {
            if (imageContainer)
            {
                _image = new Image();
                _image.setData(imageContainer);
                _imageControl.image(_image);
            }
            else
            {
                _imageControl.image(null);
            }
        }
        catch (Exception::Warning)
        {
            throw error(strFmt("@SYS19312", imageFilePathName));
        }
    }

    public void deleteImage(FormWindowControl _imageControl, CompanyImageType _imageType)
    {
        imageContainer = conNull();
        imageFilePathName = '';
        this.showImage(_imageControl);
    }

    public void getImagesData()
    {
        FormDataSource          CustTable_ds    =   this.dataSource(formDataSourceStr(CustTable, CustTable)) as FormDataSource;
        CustTable               _CustTable      =   CustTable_ds.cursor();
        FormWindowControl       LogoImage       =   this.design().controlName(formControlStr(CustTable, LogoImage)) as FormWindowControl;
        FormTabPageControl      ImagesColors    =   this.design().controlName(formControlStr(CustTable, ImagesColors)) as FormTabPageControl;

        this.parmimageContainer (conNull());
        if (_CustTable)
        {
            this.parmimageContainer(_CustTable.DemoLogo);
        }
        this.showImage(LogoImage);
        
    }
}