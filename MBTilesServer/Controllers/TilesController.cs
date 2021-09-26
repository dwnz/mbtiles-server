using System.Data.SQLite;
using Dapper;
using MBTilesServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBTilesServer.Controllers
{
    public class TilesController : Controller
    {
        public IActionResult Index(int zoom, int column, int row)
        {
            string databaseName = ConfigurationManager.Read("Tiles:Path");
            databaseName = databaseName.Replace("{zoom}", zoom.ToString());

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source=={databaseName}"))
            {
                TileModel tile = connection.QuerySingle<TileModel>(
                    "select * from tiles where zoom_level = @zoom and tile_column = @column and tile_row = @row",
                    new
                    {
                        zoom,
                        column,
                        row
                    }
                );

                return File(tile.tile_data, "image/png");
            }
        }
    }
}