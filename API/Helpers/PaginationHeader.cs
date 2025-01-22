using System;

namespace API.Helpers;

public class PaginationHeader(int currentPage, int ItemsPerPage, int totalItems, int totalPages)

{
    public int CurrentPage { get; set; } = currentPage;
    public int ItemsPerPage { get; set; } = ItemsPerPage;
    public int totalItems { get; set; } = totalItems;
    public int TotalPages { get; set; } = totalPages;
}
