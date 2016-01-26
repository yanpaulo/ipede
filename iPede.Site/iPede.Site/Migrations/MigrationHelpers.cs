using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site.Migrations
{
    public class MigrationHelpers
    {
        public static string DropDefaultConstraint(string tableName, string columnName)
        {
            string constraintVariableName = string.Format("@constraint_{0}", Guid.NewGuid().ToString("N"));

            string sql = string.Format(@"
            DECLARE {0} nvarchar(128)
            SELECT {0} = name
            FROM sys.default_constraints
            WHERE parent_object_id = object_id(N'{1}')
            AND col_name(parent_object_id, parent_column_id) = '{2}';
            IF {0} IS NOT NULL
                EXECUTE('ALTER TABLE {1} DROP CONSTRAINT ' + {0})",
                constraintVariableName,
                tableName,
                columnName);

            return sql;
        }
    }
}