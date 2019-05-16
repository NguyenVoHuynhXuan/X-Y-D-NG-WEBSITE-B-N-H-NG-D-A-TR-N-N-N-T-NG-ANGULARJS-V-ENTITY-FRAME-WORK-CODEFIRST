namespace uStora.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListShoppingCartSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("ListShoppingCart",
                p => new
                {
                    UserId = p.String()
                },
                @"select p.Image,p.Name,odt.Quantity,p.ID as ProductId,p.Alias,odt.Price,od.PaymentStatus 
                  from OrderDetails odt
                   inner join Products p on p.ID = odt.ProductID
                   inner join Orders od on od.ID = odt.OrderID
                   inner join ApplicationUsers u on u.Id = od.CustomerId
                   where (od.PaymentStatus = 0 or od.PaymentStatus = 1)
                   and od.CustomerId = @UserId");
        }

        public override void Down()
        {
            DropStoredProcedure("ListShoppingCart");
        }
    }
}
