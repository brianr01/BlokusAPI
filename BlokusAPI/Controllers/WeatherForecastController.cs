using BlokusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlokusAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var board = new Board();

            // board.updateEmptySquare(0, 0, Board.Board.Colors.Blue);
            // board.updateEmptySquare(0, 1, Board.Board.Colors.Red);
            //int[,] locations = new int[,]
            //{
            //    { 10, 10 },
            //    { 10, 11 },
            //    { 10, 12 },
            //    { 10, 13 },
            //};
            //board.updateSquares(locations, Board.Board.Colors.Green);
            List<int[,]> RTN = BlokusAPI.Services.Pieces.GetSymmetriesOutput(BlokusAPI.Services.Pieces.TetrominoL, BlokusAPI.Services.Pieces.TetrominoLSymmetries);

            int[,] piece = BlokusAPI.Services.Pieces.RotatePiece(BlokusAPI.Services.Pieces.PentominoL, 1);
            board.PlacePiece(10, 10, RTN.ElementAt(0), BlokusAPI.Services.Board.Colors.Blue);
            // board.placePiece(10, 10, Pieces.Pieces.onePiece, Board.Board.Colors.Blue);
            // board.placePiece(9, 11, Pieces.Pieces.onePiece, Board.Board.Colors.Red);
            board.PlacePiece(0, 12, RTN.ElementAt(3), BlokusAPI.Services.Board.Colors.Blue);
            board.PlacePiece(0, 16, RTN.ElementAt(4), BlokusAPI.Services.Board.Colors.Blue);
            board.PlacePiece(4, 0, RTN.ElementAt(5), BlokusAPI.Services.Board.Colors.Blue);
            board.PlacePiece(4, 4, RTN.ElementAt(6), BlokusAPI.Services.Board.Colors.Blue);
            board.PlacePiece(4, 8, RTN.ElementAt(7), BlokusAPI.Services.Board.Colors.Blue);
            // List<int[]> playable = board.getPlayableNodes(Board.Board.Colors.Blue);
            // board.updateEmptySquaresFromList(playable, Board.Board.Colors.Red);


            return board.GetBoardString();
        }
    }
}
