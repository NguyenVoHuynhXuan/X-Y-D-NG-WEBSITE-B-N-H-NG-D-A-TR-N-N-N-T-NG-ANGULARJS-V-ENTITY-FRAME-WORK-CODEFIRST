using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using uStora.Model.Models;
using uStora.Service.ExportImport.Help;

namespace uStora.Service.ExportImport
{
    public interface IExportManagerService
    {
        string ExportProductsToXml(IList<Product> products);

        byte[] ExportProductsToXlsx(IEnumerable<Product> products);

        void ExportProductsToXlsxApi(IEnumerable<Product> products, string filePath);
    }

    public class ExportManagerService : IExportManagerService
    {
        private IProductService _productService;

        public ExportManagerService(IProductService productService)
        {
            _productService = productService;
        }

        protected virtual void SetCaptionStyle(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            style.Font.Bold = true;
        }

        protected virtual byte[] ExportToXlsx<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport)
        {
            using (var stream = new MemoryStream())
            {
                // ok, we can run the real code of the sample now
                using (var xlPackage = new ExcelPackage(stream))
                {
                    // uncomment this line if you want the XML written out to the outputDir
                    //xlPackage.DebugMode = true;

                    // get handle to the existing worksheet
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name + "s");
                    //create Headers and format them

                    var manager = new PropertyManager<T>(properties.Where(p => !p.Ignore).ToArray());
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var items in itemsToExport)
                    {
                        manager.CurrentObject = items;
                        manager.WriteToXlsx(worksheet, row++);
                    }

                    xlPackage.Save();
                }
                return stream.ToArray();
            }
        }

        protected virtual void GenerateXLSAsync<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport, string filePath)
        {
            using (var stream = new MemoryStream())
            {
                // ok, we can run the real code of the sample now
                using (var xlPackage = new ExcelPackage(stream))
                {
                    // uncomment this line if you want the XML written out to the outputDir
                    //xlPackage.DebugMode = true;
                    // get handle to the existing worksheet
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name + "s");
                    //create Headers and format them
                    worksheet.Cells.AutoFitColumns();
                    var manager = new PropertyManager<T>(properties.Where(p => !p.Ignore).ToArray());
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var items in itemsToExport)
                    {
                        manager.CurrentObject = items;
                        manager.WriteToXlsx(worksheet, row++);
                    }

                    xlPackage.SaveAs(new FileInfo(filePath));
                }
            }
        }

        public string ExportProductsToXml(IList<Product> products)
        {
            return "";
        }

        public byte[] ExportProductsToXlsx(IEnumerable<Product> products)
        {
            var properties = new[]
            {
               new PropertyByName<Product>("ID",p=>p.ID,false),
               new PropertyByName<Product>("Name",p=>p.Name,false),
               new PropertyByName<Product>("Alias",p=>p.Alias,false),
               new PropertyByName<Product>("CategoryID",p=>p.CategoryID,false),
               new PropertyByName<Product>("Image",p=>p.Image,false),
               new PropertyByName<Product>("MoreImages",p=>p.MoreImages,false),
               new PropertyByName<Product>("Price",p=>p.Price,false),
               new PropertyByName<Product>("OriginalPrice",p=>p.OriginalPrice,false),
               new PropertyByName<Product>("PromotionPrice",p=>p.PromotionPrice,false),
               new PropertyByName<Product>("Warranty",p=>p.Warranty,false),
               new PropertyByName<Product>("Description",p=>p.Description,false),
               new PropertyByName<Product>("Content",p=>p.Content,false),
                new PropertyByName<Product>("HotFlag",p=>p.HotFlag,false),
               new PropertyByName<Product>("HomeFlag",p=>p.HomeFlag,false),
               new PropertyByName<Product>("ViewCount",p=>p.ViewCount,false),
               new PropertyByName<Product>("Quantity",p=>p.Quantity,false),
               new PropertyByName<Product>("BrandID",p=>p.BrandID,false),
               new PropertyByName<Product>("Tags",p=>p.Tags,false),
               new PropertyByName<Product>("MetaDescription",p=>p.MetaDescription,false),
               new PropertyByName<Product>("MetaKeyword",p=>p.MetaKeyword,false),
               new PropertyByName<Product>("CreatedDate",p=>p.CreatedDate,false),
               new PropertyByName<Product>("CreatedBy",p=>p.CreatedBy,false),
               new PropertyByName<Product>("Status",p=>p.Status,false)
            };
            var productList = products.ToList();
            return ExportToXlsx(properties, productList);
        }

        public void ExportProductsToXlsxApi(IEnumerable<Product> products, string filePath)
        {
            var properties = new[]
            {
               new PropertyByName<Product>("ID",p=>p.ID,false),
               new PropertyByName<Product>("Name",p=>p.Name,false),
               new PropertyByName<Product>("Alias",p=>p.Alias,false),
               new PropertyByName<Product>("CategoryID",p=>p.CategoryID,false),
               new PropertyByName<Product>("Image",p=>p.Image,false),
               new PropertyByName<Product>("MoreImages",p=>p.MoreImages,false),
               new PropertyByName<Product>("Price",p=>p.Price,false),
               new PropertyByName<Product>("OriginalPrice",p=>p.OriginalPrice,false),
               new PropertyByName<Product>("PromotionPrice",p=>p.PromotionPrice,false),
               new PropertyByName<Product>("Warranty",p=>p.Warranty,false),
               new PropertyByName<Product>("Description",p=>p.Description,false),
               new PropertyByName<Product>("Content",p=>p.Content,false),
                new PropertyByName<Product>("HotFlag",p=>p.HotFlag,false),
               new PropertyByName<Product>("HomeFlag",p=>p.HomeFlag,false),
               new PropertyByName<Product>("ViewCount",p=>p.ViewCount,false),
               new PropertyByName<Product>("Quantity",p=>p.Quantity,false),
               new PropertyByName<Product>("BrandID",p=>p.BrandID,false),
               new PropertyByName<Product>("Tags",p=>p.Tags,false),
               new PropertyByName<Product>("MetaDescription",p=>p.MetaDescription,false),
               new PropertyByName<Product>("MetaKeyword",p=>p.MetaKeyword,false),
               new PropertyByName<Product>("CreatedDate",p=>p.CreatedDate,false),
               new PropertyByName<Product>("CreatedBy",p=>p.CreatedBy,false),
               new PropertyByName<Product>("Status",p=>p.Status,false)
            };
            GenerateXLSAsync(properties, products, filePath);
        }
    }
}