using ScrumBoardLibrary.Board;
using ScrumBoardLibrary.Column;
namespace ScrumBoardAPI.DTO;

public class BoardDTO
{
    public BoardDTO(IBoard board)
    {
        UnicalID = board.UnicalID;
        BoardName = board.BoardName;

        List<ColumnsDTO> listColumnDTO = new List<ColumnsDTO>();
        List<IColumn> listColumn = board.LookAllColumn();

        foreach (IColumn column in listColumn)
        {
            listColumnDTO.Add(new ColumnsDTO(column));
        }
        Columns = listColumnDTO;
    }

    public string UnicalID { get; set; }
    public string BoardName { get; set; }
    public IEnumerable<ColumnsDTO> Columns { get; set; }
}
