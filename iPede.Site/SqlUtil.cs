using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site
{
    /// <summary>
    /// Sql Utility Class (mostry methods which return SQL scripts based on parameters)
    /// </summary>
    public class SqlUtil
    {
        /// <summary>
        /// Return a script to drop the DEFAULT constraint for a given table/column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string DropDefault(string table, string column)
        {
            return string.Format(
@"DECLARE @ObjectName NVARCHAR(100)
SELECT @ObjectName = OBJECT_NAME([default_object_id]) FROM SYS.COLUMNS
WHERE [object_id] = OBJECT_ID('{0}') AND [name] = '{1}';
IF @ObjectName IS NOT NULL
BEGIN
EXEC('ALTER TABLE {0} DROP CONSTRAINT ' + @ObjectName)
END", table, column);
        }
    }
}