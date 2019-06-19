using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BDC_V1.Databases
{
    /// <summary>
    /// Container for BRED database
    /// </summary>
    public class BredDatabase : Database
    {
        public BredDatabase(string bredFilename)
            : base(bredFilename)
        {
            Debug.Assert(IsValidDatabase(this));
        }

        /// <summary>
        /// Returns just the Inspectors from a BRED database
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
        /// Returns just the Inspectors from a BRED database
        /// </summary>
        /// <param name="bredFilename">Database filename</param>
        /// <returns>A DataTable with the query results, an empty DataTable on errors</returns>
        [NotNull]
        public static DataTable GetInspectors([NotNull] string bredFilename)
        {
            if (IsValidDatabase(bredFilename))
            {
                try
                {
                    var bredDatabase = new Database(bredFilename);
                    return GetInspectors(bredDatabase);
                }
                catch (Exception e)
                {
                    // ignore all exceptions
                    Debug.WriteLine(e);
                }
            }

            return new DataTable();
        }

        [NotNull]
        private static DataTable GetInspectors([NotNull] Database database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));

            return database.GetTableFromQuery(
                "SELECT * FROM [UserAccount] ORDER BY LastName, FirstName",
                null,
                CommandType.Text);
        }

        /// <summary>
        /// Validates the BRED database
        /// </summary>
        /// <returns>true = valid database</returns>
        public override bool IsValidDatabase()
        {
            try
            {
                return IsValidDatabase(this);
            }
            catch (Exception e)
            {
                // ignore all exceptions
                Debug.WriteLine(e);
            }

            return false;
        }

        /// <summary>
        /// Validates the BRED database
        /// </summary>
        /// <param name="bredFilename">filename of the database to validate</param>
        /// <returns>true = valid database</returns>
        public new static bool IsValidDatabase([NotNull] string bredFilename)
        {
            if (Database.IsValidDatabase(bredFilename))
            {
                try
                {
                    var bredDatabase = new Database(bredFilename);
                    return IsValidDatabase(bredDatabase);
                }
                catch (Exception e)
                {
                    // ignore all exceptions
                    Debug.WriteLine(e);
                }
            }

            return false;
        }

        private static bool IsValidDatabase([NotNull] Database database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));

            // TODO: Validate database file
            return database.IsValidDatabase();
        }
    }
}
