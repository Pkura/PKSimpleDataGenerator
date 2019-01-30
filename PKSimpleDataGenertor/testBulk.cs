using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PKSimpleDataGenerator
{
    class testBulk
    {
        void d()
        {
            using (SqlBulkCopy bulkCopy =
                new SqlBulkCopy(destinationConnection))
            {
                bulkCopy.DestinationTableName =
                    "dbo.BulkCopyDemoMatchingColumns";

                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer()
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Close the SqlDataReader. The SqlBulkCopy
                    // object is automatically closed at the end
                    // of the using block.
                    reader.Close();
                }
            }
        }
    }
}
