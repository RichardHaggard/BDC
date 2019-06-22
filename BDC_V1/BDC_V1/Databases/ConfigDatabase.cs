using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDC_V1.Classes;
using BDC_V1.Interfaces;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    public class ConfigDatabase : BuilderDatabase
    {
        private static readonly List<IInspector> MockUsers;

        static ConfigDatabase()
        {
            MockUsers = new List<IInspector>
            {
                new Inspector {FirstName = "Rick"  , LastName = "Wakeman", Password = Inspector.HashPassword("Yes"      )},
                new Inspector {FirstName = "Keith" , LastName = "Emerson", Password = Inspector.HashPassword("ELP"      )},
                new Inspector {FirstName = "Carlos", LastName = "Santana", Password = Inspector.HashPassword("EvilWoman")},
                new Inspector {FirstName = "George", LastName = "Jetson" , Password = Inspector.HashPassword("Leroy"    )},
            };

            foreach (var inspector in MockUsers)
            {
                inspector.UserId          = new Guid();
                inspector.PasswordChanged = DateTime.Now;
            }
        }

        public ConfigDatabase(string bredFilename)
            : base(bredFilename)
        {
            Debug.Assert(IsValidDatabase(this));
        }

        /// <summary>
        /// Returns just the Inspectors from a BRED builderDatabase
        /// </summary>
        /// <returns>A DataTable with the query results, an empty DataTable on errors</returns>
        [NotNull]
        public DataTable GetInspectors()
        {
            try
            {
                return GetInspectors(this);
            }
            catch (Exception e)
            {
                // ignore all exceptions
                Debug.WriteLine(e);
            }

            return new DataTable();
        }

        /// <summary>
        /// Returns just the Inspectors from a BRED builderDatabase
        /// </summary>
        /// <param name="configFilename">BuilderDatabase filename</param>
        /// <returns>A DataTable with the query results, an empty DataTable on errors</returns>
        [NotNull]
        public static DataTable GetInspectors([NotNull] string configFilename)
        {
            if (IsValidDatabase(configFilename))
            {
                try
                {
                    var configDatabase = new BuilderDatabase(configFilename);
                    return GetInspectors(configDatabase);
                }
                catch (Exception e)
                {
                    // ignore all exceptions
                    Debug.WriteLine(e);
                }
            }

            return new DataTable();

            // ReSharper disable once AssignNullToNotNullAttribute
            //return ConfigDatabase.GetInspectors((BuilderDatabase) null);
        }

        [NotNull]
        private static DataTable GetInspectors([NotNull] BuilderDatabase builderDatabase)
        {
            //if (builderDatabase == null) throw new ArgumentNullException(nameof(builderDatabase));

            //return builderDatabase.GetTableFromQuery(
            //    "SELECT * FROM [UserAccount] ORDER BY LastName, FirstName",
            //    null,
            //    CommandType.Text);

            var mockInspectors = new DataTable();
            mockInspectors.Columns.Add(new DataColumn("User_ID"        , typeof(Guid    )));
            mockInspectors.Columns.Add(new DataColumn("UserName"       , typeof(string  )));
            mockInspectors.Columns.Add(new DataColumn("LastName"       , typeof(string  )));
            mockInspectors.Columns.Add(new DataColumn("FirstName"      , typeof(string  )));
            mockInspectors.Columns.Add(new DataColumn("PasswordChanged", typeof(DateTime)));
            mockInspectors.Columns.Add(new DataColumn("PasswordHashed" , typeof(string  )));
            mockInspectors.Columns.Add(new DataColumn("Disabled"       , typeof(bool    )));

            foreach (var inspector in MockUsers)
            {
                var row = mockInspectors.NewRow();
                row["User_ID"        ] = inspector.UserId;
                row["UserName"       ] = inspector.FirstLast;
                row["LastName"       ] = inspector.LastName;
                row["FirstName"      ] = inspector.FirstName;
                row["PasswordChanged"] = inspector.PasswordChanged;
                row["PasswordHashed" ] = inspector.Password;
                row["Disabled"       ] = false;

                mockInspectors.Rows.Add(row);
            }
            
            return mockInspectors;
        }

        /// <summary>
        /// Validates the BRED builderDatabase
        /// </summary>
        /// <returns>true = valid builderDatabase</returns>
        public override bool IsValidDatabase()
        {
            //try
            //{
            //    return IsValidDatabase(this);
            //}
            //catch (Exception e)
            //{
            //    // ignore all exceptions
            //    Debug.WriteLine(e);
            //}

            //return false;

            return true;
        }

        /// <summary>
        /// Validates the BRED builderDatabase
        /// </summary>
        /// <param name="configFilename">filename of the builderDatabase to validate</param>
        /// <returns>true = valid builderDatabase</returns>
        public new static bool IsValidDatabase([NotNull] string configFilename)
        {
            if (BuilderDatabase.IsValidDatabase(configFilename))
            {
                try
                {
                    var configDatabase = new BuilderDatabase(configFilename);
                    return IsValidDatabase(configDatabase);
                }
                catch (Exception e)
                {
                    // ignore all exceptions
                    Debug.WriteLine(e);
                }
            }

            return false;

            //return true;
        }

        private static bool IsValidDatabase([NotNull] BuilderDatabase builderDatabase)
        {
            //if (builderDatabase == null) throw new ArgumentNullException(nameof(builderDatabase));

            //// TODO: Validate builderDatabase file
            //return builderDatabase.IsValidDatabase();

            return true;
        }

    }
}
