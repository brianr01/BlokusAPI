using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlokusAPI.Board;

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
            var board = new Board.Board();
            board.updateEmptySquare(0, 0, Board.Board.Colors.Blue);
            board.updateEmptySquare(0, 1, Board.Board.Colors.Red);
            int[,] locations = new int[,]
            {
                { 10, 10 },
                { 10, 11 },
                { 10, 12 },
                { 10, 13 },
            };
            board.updateSquares(locations, Board.Board.Colors.Green);
            int[,] piece = Pieces.Pieces.mirrorPieceHorizontally(Pieces.Pieces.rotatePiece(Pieces.Pieces.pentominoF, 3));
            board.placePiece(7, 7, piece, Board.Board.Colors.Blue);

            return board.getBoardString();
        }
    }
}
