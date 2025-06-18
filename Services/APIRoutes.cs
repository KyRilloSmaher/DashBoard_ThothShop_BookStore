using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ThothStore_DashBoard.Models.Category;
using ThothStore_DashBoard.Models.Order;
using static System.Net.WebRequestMethods;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ThothStore_DashBoard.Services
{
    public static class APIRoutes
    {
        private const string BaseUrl = "/ThothShop/V1/";

        // Authentication
        private const string AuthenticationBaseUrl = BaseUrl + "Authentication/";
        public const string Login = AuthenticationBaseUrl + "Login";
        public const string Resetpassword = AuthenticationBaseUrl + "Reset-Password";
        public const string ConfirmResetPasswordCode = AuthenticationBaseUrl + "Confirm-Reset-Password-Code";
        public const string SendResetCode = AuthenticationBaseUrl + "Send-Reset-Code";

        // Author
        private const string AuthorBaseUrl = BaseUrl + "Authors/";
        public const string GetAllPaginatedAuthors = AuthorBaseUrl + "Paginted?PageNumber={PageNumber}&PageSize={PageSize}";
        public const string GetTotalNumberOfAuthors = AuthorBaseUrl + "Author-count";
        public const string GetMostPopularAuthors = AuthorBaseUrl + "Most-Popular-Authors?PageNumber={PageNumber}&PageSize={PageSize}";
        public const string GetAuthorById = AuthorBaseUrl + "{Id}";
        public const string CreateAuthor = AuthorBaseUrl + "Create";
        public const string UpdateAuthor = AuthorBaseUrl + "Update";
        public const string DeleteAuthor = AuthorBaseUrl + "Delete/{Id}";
        public const string SearchForAuthors = AuthorBaseUrl + "Search-Authors?searchTerm={searchTerm}";
        public const string GetAuthorBooks = AuthorBaseUrl + "Author/{Id}";


        // Book
        private const string BookBaseUrl = BaseUrl + "Books/";
        public const string  GetallPaginatedBooks = BookBaseUrl + "All?PageNumber={PageNumber}&PageSize={PageSize}";
        public const string GetTotalNumberOfBooks = BookBaseUrl + "Total-Number-Of-Books";
        public const string GetOutOfStockBooks = BookBaseUrl + "out-of-stock";
        public const string GetTopSellingBooks = BookBaseUrl + "top-selling";
        public const string SearchForBooks = BookBaseUrl + "Search?searchTerm={searchTerm}";
        public const string GetBookById = BookBaseUrl + "{Id}";
        public const string CreateBook = BookBaseUrl + "Create";
        public const string UpdateAnExistingBook = BookBaseUrl + "Update";
        public const string DeleteBook = BookBaseUrl + "Delete/{Id}";

        // category
        private const string CategoryBaseUrl = BaseUrl + "Categories/";
        public const string getAllCategories = CategoryBaseUrl + "All";
        public const string getCategoryByName = CategoryBaseUrl + "Name/{term}";
        public const string createCategory = CategoryBaseUrl + "Create";
        public const string getCategoryById = CategoryBaseUrl + "{Id}";
        public const string updateCategory = CategoryBaseUrl + "Update";
        public const string deleteCategoryById = CategoryBaseUrl + "Delete/{Id}";

        //Order
        private const string OrderBaseUrl = BaseUrl + "Orders/";
        public const string GetAllPaginatedOrders = OrderBaseUrl + "All?PageNumber={PageNumber}&PageSize={PageSize}";
        public const string GetTotalNumberOfOrders = OrderBaseUrl + "Orders-count";
        public const string GetTotalSales = OrderBaseUrl + "Total-Sales";
        public const string GetOrderById = OrderBaseUrl + "{Id}";
        public const string GetOrderByStatus = OrderBaseUrl + "status/{status}";
        public const string GetRecentOrders = OrderBaseUrl + "Recent-orders";
        public const string UpdateOrderStatus = OrderBaseUrl + "{Id}/update-status";
        public const string DeleteOrder = OrderBaseUrl + "Delete/{Id}";

        // User
        private const string UserBaseUrl = BaseUrl + "Users/";
        public const string getAllAdmins = UserBaseUrl + "All-Admins";
        public const string getAllUsers = UserBaseUrl + "All-Users";
        public const string getUserById = UserBaseUrl + "{Id}";
        public const string deleteUserById = UserBaseUrl + "Delete/{Id}";
        public const string updateUser = UserBaseUrl + "Update/";
        public const string registerAdmin = UserBaseUrl + "Register-Admin";

    }
}
