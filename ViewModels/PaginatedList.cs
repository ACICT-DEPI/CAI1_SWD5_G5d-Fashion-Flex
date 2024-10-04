using Fashion_Flex.Models;

public class PaginatedList<T> : List<T>
{
	private IQueryable<Product> products;
	private int pageSize;

	public int PageIndex { get; private set; }
	public int TotalPages { get; private set; }
	public bool HasPreviousPage => PageIndex > 1;
	public bool HasNextPage => PageIndex < TotalPages;

	public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
	{
		PageIndex = pageIndex;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);

		// Add items to the list
		this.AddRange(items);
	}
}
