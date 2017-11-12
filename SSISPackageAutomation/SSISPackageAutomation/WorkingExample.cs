using System;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.Data.OleDb;
using System.Data;
using System.Runtime.InteropServices;
using System.Reflection;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;

/// <summary>
/// Summary description for CreateAndExecutePackage
/// </summary>
public class WorkingExample
{
    //Check column names from the template. 
    //DataSet dsData = ds.Clone();
    public static readonly string ExcelConnectionString = "";
    //Database Table
    DataTable dtDBTable;
    DataTable dtSetDataTypeProperties = new DataTable();

    public DataTable getExcelFields()
    {

        //DataSet Excel Source
        //ArrayList arrExcelFields = new ArrayList();
        int iRow = 0;
        DataTable dtExcelFields = new DataTable();
        dtExcelFields.Columns.Add("ExcelFields", typeof(string));
        dtExcelFields.Columns.Add("DataType", typeof(string));
        DataSet dsTemplate = GetExcelData(@"C:\vamshi\SampleInput.xls");
        DataTable dt = dsTemplate.Tables[0];
        foreach (DataColumn dc in dt.Columns)
        {
            dtExcelFields.Rows.Add();
            dtExcelFields.Rows[iRow]["ExcelFields"] = dc.ColumnName;
            dtExcelFields.Rows[iRow]["DataType"] = dc.DataType;
            //arrExcelFields.Add(dc.ColumnName);
            iRow = iRow + 1;
        }
        return dtExcelFields;
    }

    public DataTable getDataTableFields()
    {
        int iRow = 0;
        DataTable dtDBFields = new DataTable();
        dtDBFields.Columns.Add("DBFields", typeof(string));
        dtDBFields.Columns.Add("DataType", typeof(string));
        DataTable dt = GetDBColumnNames();
        foreach (DataColumn dc in dt.Columns)
        {
            dtDBFields.Rows.Add();
            dtDBFields.Rows[iRow]["DBFields"] = dc.ColumnName;
            dtDBFields.Rows[iRow]["DataType"] = dc.DataType;
            iRow = iRow + 1;
        }
        return dtDBFields;
    }

    public void CreateSaveExecutePackage(DataTable dtMappedExcelToDB, DataTable dtExcelFields, DataTable dtDBfields)
    {
        DataTable dtGetSourceTblWithDestTblColumnNames = new DataTable();
        try
        {
            dtSetDataTypeProperties.Columns.Add("length", typeof(string));
            dtSetDataTypeProperties.Columns.Add("scale", typeof(string));
            dtSetDataTypeProperties.Columns.Add("precision", typeof(string));
            dtSetDataTypeProperties.Columns.Add("codepage", typeof(string));
            Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();
            //Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();

            //To Create a package named [Sample Package]
            Package package = new Package();
            package.Name = "Sample Package";
            package.PackageType = DTSPackageType.DTSDesigner;
            package.VersionBuild = 1;

            //To add package variables
            Variable RowCountVar = package.Variables.Add("RowCountVar", false, "User", 0);
            RowCountVar.Name = @"RowCountVar";

            Variable TransactionID = package.Variables.Add("TransactionID", false, "User", 0);
            TransactionID.Name = @"TransactionID";

            Variable CurrentExcelPath = package.Variables.Add("CurrentExcelPath", false, "User", @"C:\vamshi");
            CurrentExcelPath.Name = @"CurrentExcelPath";
            //DataType will be automatically set to String

            //For source database (Excel)
            ConnectionManager ExcelSource = package.Connections.Add("EXCEL");

            //correct
            //ExcelSource.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:Site work\SampleExcelFiles\India.xls;Extended Properties=""EXCEL 8.0;HDR=YES"";";
            // Properties=\"EXCEL 8.0;HDR=YES;IMEX=1;\";"
            ExcelSource.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=+ @[User::CurrentExcelPath] +;Extended Properties=\""EXCEL 8.0;HDR=YES;IMEX=1\"";";
            //working//"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\iPlan\Site work\SampleExcelFiles\India.xls;Extended Properties=""EXCEL 8.0;HDR=YES"";";
            ExcelSource.Name = "Excel Connection Manager";
            //working//ExcelSource.SetExpression("ConnectionString", @"""Provider=Microsoft.Jet.OLEDB.4.0;Data Source="" + @[User::CurrentExcelPath] + "";Extended Properties=\""Excel 8.0;HDR=Yes\""""");
            ExcelSource.SetExpression("ConnectionString", @"""Provider=Microsoft.Jet.OLEDB.4.0;Data Source="" + @[User::CurrentExcelPath] + "";Extended Properties=\""Excel 8.0;HDR=Yes;IMEX=1\""""");
            ExcelSource.DelayValidation = true;

            //For destination database (OLAP)SQLNCLI10 SQLNCLI10 SQLNCLI.1 
            ConnectionManager OLAP = package.Connections.Add("OLEDB");
            //OLAP.ConnectionString = @"Data Source=PASDTP196\\SQLEXPRESS;Initial Catalog=Role;Provider=SQLNCLI.1;Integrated Security=True;Auto Translate=False;";
            // OLAP.ConnectionString = @"Data Source=PASDTP196\\SQLEXPRESS;Initial Catalog=Role;Provider=SQLNCLI.1;Integrated Security=True;Auto Translate=False;";

            // OLAP.ConnectionString = @"Data Source=(local);Initial Catalog=AdventureWorks;Provider=SQLNCLI.1;Integrated Security=True;Trusted_Connection=True;Auto Translate=False;pooling=false;";

            //OLAP.ConnectionString = @"Data Source=192.168.1.5;Provider=SQLOLEDB; Initial Catalog=AdventureWorks;User Id=sa;Password=password@123;Persist Security Info=True";
            //ConfigurationManager.AppSettings["AdventureWorksConnectionString"].ToString();
            //working //@"Data Source=192.168.1.5;Provider=SQLOLEDB; Initial Catalog=AdventureWorks;User Id=sa;Password=password@123;Persist Security Info=True";
            //@"Data Source=ts-dev1;Provider=SQLOLEDB;Initial Catalog=AdventureWorks;User Id=sa;Password=password@123;Integrated Security=False;";
            //OLAP.ConnectionString = @"Data Source=(localhost);Initial Catalog=AdventureWorks;User Id=sa;Password=password@123;Integrated Security=False;Auto Translate=False;";
            //correct
            //OLAP.ConnectionString = @"Data Source=PASDTP196\SQLEXPRESS;Initial Catalog=AdventureWorks;Integrated Security=SSPI;";

            OLAP.ConnectionString = @"Data Source=ebi-etl-dev-01;Initial Catalog=LDAP;Provider=SQLOLEDB.1;Integrated Security=SSPI;";
            OLAP.Name = "LocalHost.OLAP";


            //To add "Excel Sheet Iterator" (ForEach Loop container)
            ForEachLoop ForEachLoopContainer = (ForEachLoop)package.Executables.Add("STOCK:FOREACHLOOP");
            ForEachLoopContainer.Name = @"Excel Sheet Iterator";
            ForEachLoopContainer.Description = @"Foreach Loop Container";

            //To Set Foreach loop container collection information
            ForEachEnumeratorInfo FileEnumeratorInfo = app.ForEachEnumeratorInfos["Foreach File Enumerator"];
            ForEachEnumeratorHost FileEnumeratorHost = FileEnumeratorInfo.CreateNew();
            FileEnumeratorHost.CollectionEnumerator = false;

            //FileEnumeratorHost.Properties["Directory"].SetValue(FileEnumeratorHost, @"C:\Site work\SampleExcelFiles");

            FileEnumeratorHost.Properties["Directory"].SetValue(FileEnumeratorHost, @"C:\vamshi");
            FileEnumeratorHost.Properties["FileSpec"].SetValue(FileEnumeratorHost, @"*.xls*");
            //0 = "Fully qualified" ; 1 = "Name and extension" ; 2 = "Name only"
            FileEnumeratorHost.Properties["FileNameRetrieval"].SetValue(FileEnumeratorHost, 0);
            FileEnumeratorHost.Properties["Recurse"].SetValue(FileEnumeratorHost, "False");
            ForEachLoopContainer.ForEachEnumerator = FileEnumeratorHost;

            //To Set Foreach loop container variable mappings
            ForEachVariableMapping CurrenFileVariableMapping = ForEachLoopContainer.VariableMappings.Add();
            CurrenFileVariableMapping.VariableName = @"User::CurrentExcelPath";
            CurrenFileVariableMapping.ValueIndex = 0;

            //To add Sales inside Foreach loop container (Data Flow Task)
            TaskHost dataFlowTaskHost = (TaskHost)ForEachLoopContainer.Executables.Add("DTS.Pipeline.1");
            dataFlowTaskHost.Name = @"Role";
            dataFlowTaskHost.FailPackageOnFailure = true;
            dataFlowTaskHost.FailParentOnFailure = true;
            //dataFlowTaskHost.DelayValidation = false;
            dataFlowTaskHost.DelayValidation = true;
            dataFlowTaskHost.Description = @"Data Flow Task";
            dataFlowTaskHost.TransactionOption = DTSTransactionOption.NotSupported;

            //-----------Data Flow Inner component starts----------------
            MainPipe dataFlowTask = dataFlowTaskHost.InnerObject as MainPipe;
            // Source OLE DB connection manager to the package.
            ConnectionManager SconMgr = package.Connections["Excel Connection Manager"];
            // Destination OLE DB connection manager to the package.
            ConnectionManager DconMgr = package.Connections["LocalHost.OLAP"];

            // Create and configure an Excel source component. 
            IDTSComponentMetaData100 source = dataFlowTask.ComponentMetaDataCollection.New();
            source.ComponentClassID = "DTSAdapter.ExcelSource.1";

            // Create the design-time instance of the source.
            CManagedComponentWrapper srcDesignTime = source.Instantiate();
            // The ProvideComponentProperties method creates a default output.
            srcDesignTime.ProvideComponentProperties();
            source.Name = "Excel Source (Role)";

            // Assign the connection manager.
            source.RuntimeConnectionCollection[0].ConnectionManagerID = SconMgr.ID;
            source.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.ToConnectionManager90(SconMgr);
            // Set the custom properties of the source.
            srcDesignTime.SetComponentProperty("AccessMode", 0); // Mode 0 : OpenRowset / Table - View
            srcDesignTime.SetComponentProperty("OpenRowset", "Sheet1$");

            //srcDesignTime.SetComponentProperty("OpenRowset", GetSheetNames());
            // Connect to the data source, and then update the metadata for the source.
            srcDesignTime.AcquireConnections(null);
            srcDesignTime.ReinitializeMetaData();
            //ADDED TODAY
            IDTSExternalMetadataColumn100 exOutColumn;

            foreach (IDTSOutputColumn100 outColumn in source.OutputCollection[0].OutputColumnCollection)
            {
                exOutColumn = source.OutputCollection[0].ExternalMetadataColumnCollection[outColumn.Name];
                srcDesignTime.MapOutputColumn(source.OutputCollection[0].ID, outColumn.ID, exOutColumn.ID, true);
            }
            srcDesignTime.ReleaseConnections();

            ////addded
            //Open Excel Connection
            ////Add an Row Count to the data flow.
            //IDTSComponentMetaData90 RowCountComponent = dataFlowTask.ComponentMetaDataCollection.New();
            //RowCountComponent.Name = "Row Count";
            //RowCountComponent.ComponentClassID = "DTSTransform.RowCount.1"; //Public Token is: "{DE50D3C7-41AF-4804-9247-CF1DEB147971}";
            //CManagedComponentWrapper rowCountDesignTime = RowCountComponent.Instantiate();
            //rowCountDesignTime.ProvideComponentProperties();
            //rowCountDesignTime.SetComponentProperty("VariableName", "RowCountVar");
            //rowCountDesignTime.AcquireConnections(null);
            //rowCountDesignTime.ReinitializeMetaData();
            //rowCountDesignTime.ReleaseConnections();

            ////// Create the path from source to Row Count Transformation.
            //IDTSPath90 pathSource_RowCount = dataFlowTask.PathCollection.New();
            //pathSource_RowCount.AttachPathAndPropagateNotifications(source.OutputCollection[0], RowCountComponent.InputCollection[0]);

            // Connect the source and the transform
            //dataFlowTask.PathCollection.New().AttachPathAndPropagateNotifications(source.OutputCollection[0],conversionDataFlowComponent.InputCollection[0]);
            // Create and configure an OLE DB destination component.
            IDTSComponentMetaData100 destination = dataFlowTask.ComponentMetaDataCollection.New();
            destination.ComponentClassID = "DTSAdapter.OLEDBDestination.1";

            // Create the design-time instance of the destination.
            CManagedComponentWrapper destDesignTime = destination.Instantiate();
            // The ProvideComponentProperties method creates a default input.
            destDesignTime.ProvideComponentProperties();
            //added

            destination.Name = "Role from OLAP";
            // Assign the connection manager.
            destination.RuntimeConnectionCollection[0].ConnectionManagerID = DconMgr.ID;
            destination.RuntimeConnectionCollection[0].ConnectionManager = DtsConvert.ToConnectionManager90(DconMgr);
            // Set the custom properties.
            destDesignTime.SetComponentProperty("AccessMode", 3); // Mode 3 : OpenRowset Using FastLoad / Table - View fast load
            destDesignTime.SetComponentProperty("SqlCommand", "SELECT * FROM PRODUCTS");

            //Conversion
            IDTSComponentMetaData100 conversionDataFlowComponent = dataFlowTask.ComponentMetaDataCollection.New();// creating data conversion 
            conversionDataFlowComponent.ComponentClassID = "DTSTransform.DataConvert";//"{C3BF62C8-7C5C-4F85-83C3-E0B6F6BE267C}";// This is the GUID for data conversion component
            CManagedComponentWrapper conversionInstance = conversionDataFlowComponent.Instantiate();//Instantiate
            conversionInstance.ProvideComponentProperties();
            conversionDataFlowComponent.Name = "Conversion compoenent";

            // Create the path.
            IDTSPath100 fPath = dataFlowTask.PathCollection.New(); fPath.AttachPathAndPropagateNotifications(
            source.OutputCollection[0],
            conversionDataFlowComponent.InputCollection[0]);
            conversionInstance.AcquireConnections(null);
            conversionInstance.ReinitializeMetaData();

            ///ADDDED TODAY
            IDTSPath100 path = dataFlowTask.PathCollection.New();
            path.AttachPathAndPropagateNotifications(conversionDataFlowComponent.OutputCollection[0], destination.InputCollection[0]);
            
            // Get the output collection
            IDTSOutput100 output = conversionDataFlowComponent.OutputCollection[0];
            IDTSVirtualInput100 virtualInput = conversionDataFlowComponent.InputCollection[0].GetVirtualInput();
            int inputId = conversionDataFlowComponent.InputCollection[0].ID;
            foreach (IDTSVirtualInputColumn100 vcolumn in virtualInput.VirtualInputColumnCollection)
            {
                string DataType = "Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.";
                string finaldatatype = "";
                //IDTSExternalMetadataColumn90 exColumn = dataConvertOutput.ExternalMetadataColumnCollection.New();
                DataTable dtTempDBfields = dtDBfields.Clone();
                string formattedColName = getformmatedSourceColumn(dtMappedExcelToDB, vcolumn.Name);
                string whereclauseDBF = "DBFields = '" + formattedColName + "'";
                string bstrName = formattedColName; //vColumn.Name.ToString() + "AsString";

                foreach (DataRow DRC in dtDBfields.Select(whereclauseDBF))
                {
                    dtTempDBfields.ImportRow(DRC);
                }
                int sourceColumnLineageId = virtualInput.VirtualInputColumnCollection[vcolumn.Name.ToString()].LineageID;
                Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType dtype = new Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType();
                dtype = getSSISDataTypes(dtTempDBfields.Rows[0][1].ToString());

                IDTSInputColumn100 inputColumn = conversionInstance.SetUsageType(inputId, virtualInput, vcolumn.LineageID, DTSUsageType.UT_READONLY);
                IDTSOutputColumn100 outputColumn = conversionInstance.InsertOutputColumnAt(output.ID, 0, bstrName, string.Empty);
                conversionInstance.SetOutputColumnDataTypeProperties(output.ID, outputColumn.ID, dtype, int.Parse(dtSetDataTypeProperties.Rows[0]["length"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["precision"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["scale"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["codepage"].ToString()));
                outputColumn.CustomPropertyCollection[0].Value = inputColumn.LineageID;
                dtSetDataTypeProperties.Clear();
            }
            conversionInstance.ReleaseConnections();
            
            //Conversion
            destDesignTime.AcquireConnections(null);
            destDesignTime.ReinitializeMetaData();
            IDTSInput100 input;
            IDTSVirtualInput100 vInput;

            // Get the destination's default input and virtual input.
            input = destination.InputCollection[0];
            vInput = input.GetVirtualInput();

            // Iterate through the virtual input column collection.

            foreach (IDTSVirtualInputColumn100 vColumn in vInput.VirtualInputColumnCollection)
            {
                string whereclauseEXF = "ExcelFields = '" + vColumn.Name + "'";
                DataTable dtTempExcelFields = dtExcelFields.Clone();
                foreach (DataRow DRC in dtExcelFields.Select(whereclauseEXF))
                {
                    dtTempExcelFields.ImportRow(DRC);
                }
                if (dtTempExcelFields.Rows.Count > 0)
                {
                    continue;
                }
                else
                {
                    destDesignTime.SetUsageType(
                    input.ID, vInput, vColumn.LineageID,
                    DTSUsageType.UT_READONLY);
                }
                // culprit Line. I can found vColumn.LineageID = source lineage ids
                // while debugging.. can you please help me out here?

            }

            IDTSExternalMetadataColumn100 exColumn;
            foreach (IDTSInputColumn100 inColumn in destination.InputCollection[0].InputColumnCollection)
            {
                exColumn = destination.InputCollection[0].ExternalMetadataColumnCollection[inColumn.Name];
                destDesignTime.MapInputColumn(destination.InputCollection[0].ID, inColumn.ID, exColumn.ID);
            }
            destDesignTime.ReleaseConnections();

            // Iterate through the virtual input column collection
            //foreach (IDTSVirtualInputColumn90 vColumn in
            // vInput.VirtualInputColumnCollection)
            //{
            // Remember, we will find all columns here into
            // the 'vInput.VirtualInputColumnCollection'
            // Which is, if the total columns count into the flat file is
            // 6 then here you will
            // get 12 ( 6 * 2 ) columns into
            // the 'vInput.VirtualInputColumnCollection'. Because,
            // the data derived column usually provides all the inputs as
            // its outputs along with the outputs
            // that it really creates. And here we need to consider
            // only those inputs which came
            // from the derived column component (not from the flat file
            // source component).
            // How can we do that? we can do this by checking the
            // lineageid that we did populate during
            // the derived column creation process.
            // derivedLineageIdentifiers[outputColumn.LineageID] =
            //outputColumn.Name;
            // if (derivedLineageIdentifiers.ContainsKey(vColumn.LineageID))
            // { // if the column came from the derived column dataflow
            // // component
            // destDesignTime.
            // SetUsageType(
            // input.ID, vInput,
            // vColumn.LineageID,
            // DTSUsageType.UT_READONLY);
            //// }
            // }


            //////Add transform
            // IDTSComponentMetaData90 dataConvertComponent = dataFlowTask.ComponentMetaDataCollection.New();
            // dataConvertComponent.ComponentClassID = "DTSTransform.DataConvert";
            // dataConvertComponent.Name = "Data Convert";
            // dataConvertComponent.Description = "Data Conversion Component";

            // CManagedComponentWrapper dataConvertWrapper = dataConvertComponent.Instantiate();
            // dataConvertWrapper.ProvideComponentProperties();

            // // Connect the source and the transform
            // dataFlowTask.PathCollection.New().AttachPathAndPropagateNotifications(source.OutputCollection[0],
            // dataConvertComponent.InputCollection[0]);

            // IDTSPath90 pathRowCount_Dest = dataFlowTask.PathCollection.New();
            // pathRowCount_Dest.AttachPathAndPropagateNotifications(dataConvertComponent.OutputCollection[0], destination.InputCollection[0]);

            // ////Configure the transform
            // IDTSInput90 input = dataConvertComponent.InputCollection[0];

            // IDTSVirtualInput90 dataConvertVirtualInput = dataConvertComponent.InputCollection[0].GetVirtualInput();
            // IDTSOutput90 dataConvertOutput = dataConvertComponent.OutputCollection[0];
            //IDTSInput90 input1 = destination.InputCollection[0];
            ////IDTSInput90 input1 = destination.InputCollection[0];
            //IDTSVirtualInput90 vInput = input1.GetVirtualInput();

            // //********
            // dataConvertOutput.OutputColumnCollection.RemoveAll();
            // dataConvertOutput.ExternalMetadataColumnCollection.RemoveAll();




            //ADDED TODAY
            // IDTSComponentMetaData90 sourceDataFlowComponent = dataFlowTask.ComponentMetaDataCollection.New();

            //sourceDataFlowComponent.ComponentClassID = "{90C7770B-DE7C-435E-880E-E718C92C0573}";

            // Code for configuring the source data flow component




            //foreach (IDTSVirtualInputColumn90 vColumn in dataConvertVirtualInput.VirtualInputColumnCollection)
            //{
            //string DataType = "Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.";
            //string finaldatatype = "";
            //IDTSExternalMetadataColumn90 exColumn = dataConvertOutput.ExternalMetadataColumnCollection.New();

            //DataTable dtTempDBfields = dtDBfields.Clone();
            //string formattedColName = getformmatedSourceColumn(dtMappedExcelToDB, vColumn.Name);
            //string whereclauseDBF = "DBFields = '" + formattedColName + "'";
            //string bstrName = formattedColName; //vColumn.Name.ToString() + "AsString";
            ////

            //foreach (DataRow DRC in dtDBfields.Select(whereclauseDBF))
            //{
            // dtTempDBfields.ImportRow(DRC);
            //}
            //int sourceColumnLineageId = dataConvertVirtualInput.VirtualInputColumnCollection[vColumn.Name.ToString()].LineageID;
            //dataConvertWrapper.SetUsageType(
            // dataConvertComponent.InputCollection[0].ID,
            // dataConvertVirtualInput,
            // sourceColumnLineageId,
            // DTSUsageType.UT_READONLY);
            //IDTSOutputColumn90 newOutputColumn = dataConvertWrapper.InsertOutputColumnAt(dataConvertOutput.ID, 0, bstrName, string.Empty);

            //Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType dtype = new Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType();
            //dtype = getSSISDataTypes(dtTempDBfields.Rows[0][1].ToString());
            //newOutputColumn.SetDataTypeProperties(dtype, int.Parse(dtSetDataTypeProperties.Rows[0]["length"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["precision"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["scale"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["codepage"].ToString()));
            //dtSetDataTypeProperties.Clear();

            //dataConvertWrapper.SetOutputColumnProperty(
            // dataConvertOutput.ID,
            // newOutputColumn.ID,
            // "SourceInputColumnLineageID",
            // sourceColumnLineageId);

            //}

            //IDTSOutput90 dataConvertOutput = dataConvertComponent.OutputCollection[0];

            //IDTSInput90 input = dataConvertComponent.InputCollection[0];
            //IDTSVirtualInput90 vInput = input.GetVirtualInput();
            //IDTSOutput90 dataConvertOutput = source.OutputCollection[0];

            //////////////////////////

            // Get the destination's default input and virtual input.


            //IDTSInput90 input1 = destination.InputCollection[0];
            //IDTSVirtualInput90 vInput = input1.GetVirtualInput();
            ////IDTSOutput90 dataConvertOutput = source.OutputCollection[0];
            //IDTSOutput90 dataConvertOutput = destination.OutputCollection[0];
            //// Iterate through the virtual input column collection.
            //foreach (IDTSVirtualInputColumn90 vColumn in vInput.VirtualInputColumnCollection)
            //{
            // //IDTSInput90 inputTemp;
            // IDTSVirtualInputColumn90 vColumnTemp; 
            // string DataType = "Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.";
            // string finaldatatype = "";

            // ///
            // DataTable dtTempDBfields = dtDBfields.Clone();
            // string formattedColName = getformmatedSourceColumn(dtMappedExcelToDB, vColumn.Name);
            // string whereclauseDBF = "DBFields = '" + formattedColName + "'";
            // string bstrName = formattedColName; //vColumn.Name.ToString() + "AsString";
            // ///
            // //IDTSExternalMetadataColumn90 exColumn = dataConvertOutput.ExternalMetadataColumnCollection.New();
            // //vColumnTemp.Name = vInput.Name;
            // //vColumnTemp.ID = vInput.ID ;


            // foreach (DataRow DRC in dtDBfields.Select(whereclauseDBF))
            // {
            // dtTempDBfields.ImportRow(DRC);
            // }
            // IDTSOutputColumn90 newOutputColumn = destDesignTime.InsertOutputColumnAt(dataConvertOutput.ID, 0, bstrName, string.Empty);

            // Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType dtype = new Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType();
            // dtype = getSSISDataTypes(dtTempDBfields.Rows[0][1].ToString());
            // newOutputColumn.SetDataTypeProperties(dtype, int.Parse(dtSetDataTypeProperties.Rows[0]["length"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["precision"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["scale"].ToString()), int.Parse(dtSetDataTypeProperties.Rows[0]["codepage"].ToString()));
            // dtSetDataTypeProperties.Clear();
            // int sourceColumnLineageId = vInput.VirtualInputColumnCollection[vColumn.Name.ToString()].LineageID;
            // destDesignTime.SetUsageType(
            // input1.ID,
            // vInput,
            // sourceColumnLineageId,
            // DTSUsageType.UT_READONLY);


            // //destDesignTime.SetInputColumnProperty((
            // ////dataConvertComponent.SetOutputColumnProperty(
            // // dataConvertOutput.ID,
            // // newOutputColumn.ID,
            // // "SourceInputColumnLineageID",
            // // sourceColumnLineageId);

            // // Call the SetUsageType method of the destination
            // // to add each available virtual input column as an input column.
            // //vColumnTemp.DataType =

            // destDesignTime.SetUsageType(
            // input1.ID, vInput, vColumn.LineageID, DTSUsageType.UT_READONLY);

            //}


            ////map external metadata to the inputcolumn
            //foreach (IDTSInputColumn90 inputColumn in input1.InputColumnCollection)
            //{
            // string formattedColName = getformmatedSourceColumn(dtMappedExcelToDB, inputColumn.Name);
            // IDTSExternalMetadataColumn90 exMetaColumn = (IDTSExternalMetadataColumn90)input1.ExternalMetadataColumnCollection[formattedColName];
            // inputColumn.ExternalMetadataColumnID = exMetaColumn.ID;
            //}


            //Saving the package
            app.SaveToXml(@"E:\iPlan\Site work\SamplePackage\SamplePackage\SamplePackage.dtsx", package, null);
            //app.SaveToXml(@"\\ts-dev1\\iPlan\\Site work\\SamplePackage\\SamplePackage\\SamplePackage.dtsx", package, null);

            string pkgLocation;
            Package pkg;


            pkgLocation =
            @"E:\iPlan\Site work\SamplePackage\SamplePackage\SamplePackage.dtsx";

            Application app1 = new Application();
            pkg = app1.LoadPackage(pkgLocation, null);
            pkg.Execute();


        }

        catch (COMException ex)
        {
            string symbolicName = GetSymbolicName(ex.ErrorCode);

            System.Diagnostics.Debug.WriteLine(ex.GetType().Name);

            System.Diagnostics.Debug.WriteLine(ex.ErrorCode);
            throw (ex);
        }
    }

    public Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType getSSISDataTypes(string DataType)
    {
        int codepage = 0; ;
        int length = 0; ;
        int scale = 0;
        int precision = 0;


        Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_BYTES;
        switch (DataType)
        {
            case "System.Bit":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_BOOL;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;

                break;
            case "System.Binary":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_BYTES;
                codepage = 0;
                length = 50;
                scale = 0;
                precision = 0;
                break;
            case "System.Varbinary":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_BYTES;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;
                break;
            case "System.Timestamp":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_BYTES;
                codepage = 0;
                length = 50;
                scale = 0;
                precision = 0;
                break;

            case "System.Money":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_CY;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;
                break;
            case "System.Smallmoney":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_CY;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;
                break;
            case "System.Date":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_DBDATE;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;
                break;
            case "System.Datetime":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_DBTIMESTAMP;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;
                break;
            //case "System.Datetime2": Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_DBTIMESTAMP2;
            // codepage = 0;
            // length = 0;
            // scale = 0;
            // precision = 0;
            // break;
            case "System.Smalldatetime":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_DBTIMESTAMP;
                codepage = 0;
                length = 0;
                scale = 0;
                precision = 0;
                break;
            case "System.nchar":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR;
                codepage = 0;
                length = 3000;
                scale = 0;
                precision = 0;
                break;
            case "System.Nvarchar":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR;
                codepage = 0;
                length = 3000;
                scale = 0;
                precision = 0;
                break;
            case "System.Sql_variant":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR;
                codepage = 0;
                length = 3000;
                scale = 0;
                precision = 0;
                break;
            case "System.Xml":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR;
                codepage = 0;
                length = 3000;
                scale = 0;
                precision = 0;
                break;
            case "System.Decimal":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_NUMERIC;
                codepage = 0;
                length = 0;
                scale = 2;
                precision = 18;

                break;
            case "System.Numeric":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_NUMERIC;
                codepage = 0;
                length = 0;
                scale = 3;
                precision = 2;
                break;
            case "System.Char":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_STR;
                codepage = 1252;
                length = 4000;
                scale = 0;
                precision = 0;
                break;
            case "System.String":
                Dtype = Microsoft.SqlServer.Dts.Runtime.Wrapper.DataType.DT_WSTR;
                codepage = 0;
                length = 255;
                scale = 0;
                precision = 0;
                break;


        }
        dtSetDataTypeProperties.Rows.Add();
        dtSetDataTypeProperties.Rows[0]["length"] = length;
        dtSetDataTypeProperties.Rows[0]["scale"] = scale;
        dtSetDataTypeProperties.Rows[0]["precision"] = precision;
        dtSetDataTypeProperties.Rows[0]["codepage"] = codepage;
        return Dtype;
    }

    public string getformmatedSourceColumn(DataTable dtMappedExcelToDB, string inputColName)
    {
        string formattedColName = "";
        foreach (DataRow dr in dtMappedExcelToDB.Rows)
        {
            if (dr["Label"].ToString() == inputColName)
            {
                formattedColName = dr["Combo"].ToString();
                break;
            }
        }
        return formattedColName;
    }

    //protected string GetSheetNames(string tempfileName)
    //{

    // System.Text.StringBuilder str = new System.Text.StringBuilder();
    // SpreadsheetGear.IWorkbook Workbook;
    // Workbook = SpreadsheetGear.Factory.GetWorkbook(tempfileName);
    // for (int i = 0; i < Workbook.Worksheets.Count; i++)
    // {
    // Workbook.Worksheets[i].Visible = SpreadsheetGear.SheetVisibility.Visible;
    // string name = Workbook.Worksheets[i].Name;
    // string sSName = "[" + name.ToString() + "]";
    // sSName = sSName.Replace("'", "");
    // sSName = sSName.Replace("$", "");
    // str = str.Append(sSName + " ,");
    // }
    // return str.ToString();
    //}

    protected DataSet GetExcelData(string templateName)
    {
        DataSet ds = null;
        OleDbConnection _objExcelConn = new OleDbConnection();
        string _ExcelConnectionString = ExcelConnectionString.Replace("@FileName", templateName);
        _objExcelConn.ConnectionString = _ExcelConnectionString;
        try
        {
            ds = new DataSet();
            OleDbCommand command = new OleDbCommand("Select * FROM [" + "Sheet1" + "$]", _objExcelConn);
            _objExcelConn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            da.Fill(ds);

        }
        catch (Exception e)
        {
            throw (e);
        }
        finally
        {
            _objExcelConn.Close();
            _objExcelConn.Dispose();
        }
        return ds;
    }

    protected DataTable GetDBColumnNames()
    {
        DataSet ds = new DataSet();
        DataTable dt = null;
        OleDbConnection conn = new OleDbConnection(string.Format("Data Source={0};Initial Catalog={1};Integrated Security=TRUE;", "EBI-ETL-DEV-01", "LDAP"));
        try
        {
            OleDbCommand cmd = new OleDbCommand("Select * from Role", conn);
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(ds);
            dt = ds.Tables[0];
        }
        catch (Exception e)
        {
            throw (e);
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return dt;

    }

    public static string GetSymbolicName(int errorCode)
    {
        string symbolicName = string.Empty;
        HResults hresults = new HResults();

        foreach (FieldInfo fieldInfo in hresults.GetType().GetFields())
        {
            if ((int)fieldInfo.GetValue(hresults) == errorCode)
            {
                symbolicName = fieldInfo.Name;
                break;
            }
        }

        return symbolicName;
    }

    private class ConfigurationManager
    {
    }

    //public DataflowComponent CreateSourceDataFlowComponent
    // (DTSPackage package, DTSExecutable dataflowTask)
    //{
    // // create the component
    // DataflowComponent sourceDataFlowComponent =
    // new DataflowComponent(dataflowTask, SourceDataFlowComponentID,
    // "Source Data Flow component");

    // return sourceDataFlowComponent;
    //}

}