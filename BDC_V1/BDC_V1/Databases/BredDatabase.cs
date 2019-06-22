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
    /// Container for BRED builderDatabase
    /// </summary>
    public class BredDatabase : BuilderDatabase
    {
        public BredDatabase(string bredFilename)
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
        /// <param name="bredFilename">BuilderDatabase filename</param>
        /// <returns>A DataTable with the query results, an empty DataTable on errors</returns>
        [NotNull]
        public static DataTable GetInspectors([NotNull] string bredFilename)
        {
            if (IsValidDatabase(bredFilename))
            {
                try
                {
                    var bredDatabase = new BuilderDatabase(bredFilename);
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
        private static DataTable GetInspectors([NotNull] BuilderDatabase builderDatabase)
        {
            if (builderDatabase == null) throw new ArgumentNullException(nameof(builderDatabase));

            return builderDatabase.GetTableFromQuery(
                "SELECT * FROM [UserAccount] ORDER BY LastName, FirstName",
                null,
                CommandType.Text);
        }

        /// <summary>
        /// Validates the BRED builderDatabase
        /// </summary>
        /// <returns>true = valid builderDatabase</returns>
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
        /// Validates the BRED builderDatabase
        /// </summary>
        /// <param name="bredFilename">filename of the builderDatabase to validate</param>
        /// <returns>true = valid builderDatabase</returns>
        public new static bool IsValidDatabase([NotNull] string bredFilename)
        {
            if (BuilderDatabase.IsValidDatabase(bredFilename))
            {
                try
                {
                    var bredDatabase = new BuilderDatabase(bredFilename);
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

        private static bool IsValidDatabase([NotNull] BuilderDatabase builderDatabase)
        {
            if (builderDatabase == null) throw new ArgumentNullException(nameof(builderDatabase));

            // TODO: Validate builderDatabase file
            return builderDatabase.IsValidDatabase();
        }
    }
}
