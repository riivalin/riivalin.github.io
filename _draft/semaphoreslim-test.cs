using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(5); // 設定最大同時進行的 Task 數量為 5

    public static async Task Main(string[] args)
    {
        await FetchAndStorePlayerDataAsync();
    }

    public static async Task FetchAndStorePlayerDataAsync()
    {
        var teamList = await feijing88Repository.GetTeamListAsync();

        if (teamList != null && teamList.Any())
        {
            var tasks = teamList.Select(teamId => FetchAndStoreTeamPlayersWithLimitAsync(input));
            await Task.WhenAll(tasks);
        }
    }

    private static async Task FetchAndStoreTeamPlayersWithLimitAsync(object input)
    {
        await Semaphore.WaitAsync(); // 等待進入
        try
        {
            await FetchAndStoreTeamPlayersAsync(input);
        }
        catch(Exception ex)
        {
            logger.LogError(ex.ToString());
        }
        finally
        {
            Semaphore.Release(); // 釋放
        }
    }

    private static async Task FetchAndStoreTeamPlayersAsync(object input)
    {

            using var conn = dbHelper.DbConnection();
            await conn.ExecuteAsync("football_change_set", input, commandType: System.Data.CommandType.StoredProcedure);
    }
}