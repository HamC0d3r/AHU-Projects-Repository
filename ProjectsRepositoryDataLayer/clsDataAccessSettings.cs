using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Defines the <see cref="clsDataAccessSettings" />
    /// </summary>
    public class clsDataAccessSettings
    {
        /// <summary>
        /// Defines the ConnectionString
        /// </summary>
        public static readonly string ConnectionString = "Server=.;Database=ProjectsRepositoryDB;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
    }

}
// Copy this and paste it in a file named App.config and change the conntection string value 

/*<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>

	<appSettings>
		<add key="ConnectionString" value="Server=.;Database=ProjectsRepositoryDB;User Id=sa;Password=sa123456;"/>
	</appSettings>
</configuration>*/