using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using muehan.func;

namespace FuncDemo
{
    public static class DeleteCompleted
    {
        [FunctionName("DeleteCompleted")]
        public static async Task Run(
            [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
            [Table("todos", Connection = "AzureWebJobsStorage")] CloudTable todoTable,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            TableQuery<TodoTableEntity> rangeQuery = new TableQuery<TodoTableEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition(
                        "PartitionKey",
                        QueryComparisons.Equal,
                        "TODO"),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForBool(
                        "IsCompleted",
                        QueryComparisons.Equal,
                        true)));

            var items = await todoTable.ExecuteQuerySegmentedAsync(rangeQuery, null);

            log.LogInformation($"Delete items count: {items.Results.Count}");

            // Execute the query and loop through the results
            foreach (var entity in items.Results)
            {
                log.LogInformation($"Delete item: {entity.RowKey}");

                var deleteOperation = TableOperation
                    .Delete(
                        entity);

                var deleteResult = await todoTable.ExecuteAsync(deleteOperation);
            }
        }
    }
}
