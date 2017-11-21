using System;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Wrapper = Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Npgsql;
using System.Data;

namespace SSISPackageAutomation
{
    public class SSISConnection
    {
        // Private data.  

        private ConnectionManager ConMgr;
        // Class definition for OLE DB provider.  
        public void CreateODBCConnection(Package p)
        {
            ConMgr = p.Connections.Add("ODBC");
            ConMgr.ConnectionString = "Dsn=PostgreSQL35W;server=localhost;uid=vams3203;database=NEWDB;port=5432;sslmode=disable;readonly=0;protocol=7.4;fakeoidindex=0;showoidcolumn=0;rowversioning=0;showsystemtables=0;fetch=100;unknownsizes=0;maxvarcharsize=255;maxlongvarcharsize=8190;debug=0;commlog=0;usedeclarefetch=0;textaslongvarchar=1;unknownsaslongvarchar=0;boolsaschar=1;parse=0;lfconversion=1;updatablecursors=1;trueisminus1=0;bi=0;byteaaslongvarbinary=1;useserversideprepare=1;lowercaseidentifier=0;gssauthusegss=0;xaopt=1";
            ConMgr.Name = "SSIS Connection Manager for ODBC to connect POSTGRESQL";
            ConMgr.Description = "OLE DB connection to the PostGreSQL Database";
        }

        public void CreateADONETConnection(Package p)
        {
            ConMgr = p.Connections.Add("ADO");
            ConMgr.ConnectionString = "server=localhost;user id=root;database=world;";
            ConMgr.Name = "SSIS Connection Manager for ODBC to connect MYSQL";
            ConMgr.Description = "ADO.NET connection to the MYSQL Database";
        }

        public void CreateFileConnection(Package p)
        {
            ConMgr = p.Connections.Add("File");
            ConMgr.ConnectionString = "\\\\<yourserver>\\<yourfolder>\\books.xml";
            ConMgr.Name = "SSIS Connection Manager for Files";
            ConMgr.Description = "Flat File connection";
        }

    }
}
