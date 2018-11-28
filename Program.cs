using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCodeOdev1
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductService productService = new ProductManager(new CentalBankServiceAdapter());
            productService.Sell(new Product { Id = 1, Name = "Laptop", Price = 1000 },
                new Customer(new OfficerType()) { Id = 1, Name = "Abc" });
        }

    }

    class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
    class Customer : IEntity
    {
        ICustomerType _customerType;

        public Customer(ICustomerType customerType)
        {
            _customerType = customerType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public decimal Rate
        {
            get
            {
                return _customerType.GetRate();
            }
        }

    }

    public interface ICustomerType
    {
        decimal GetRate();
    }

    class StudentType : ICustomerType
    {
        public decimal GetRate()
        {
            return (decimal)0.9;
        }
    }
    class OfficerType : ICustomerType
    {
        public decimal GetRate()
        {
            return (decimal)0.8; ;
        }
    }
    interface IEntity
    {

    }

    class ProductManager : IProductService
    {
        IBankService _bankService;

        public ProductManager(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void Sell(Product product, Customer customer)
        {
            decimal price = product.Price;

            price = product.Price * customer.Rate;
            Console.WriteLine(price.ToString());
           
            //if (customer.CustomerType.IsStudent)
            //{
            //    price = product.Price * (decimal)0.9;
            //}
            //if (customer.CustomerType.IsOfficer)
            //{
            //    price = product.Price * (decimal)0.8;
            //}

            price = _bankService.ConvertRate(new CurrencyRate { Currency = 1, Price = 1000 });
            Console.WriteLine(price.ToString());
            Console.ReadLine();
        }
    }

    internal interface IProductService
    {
        void Sell(Product product, Customer customer);
    }

    class FakeCentralService : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)5.30;
        }
    }

    internal interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }

    class CurrencyRate
    {
        public decimal Price { get; set; }
        public int Currency { get; set; }
    }


    class CentalBankServiceAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            CentralBankService centralBankService = new CentralBankService();
            return centralBankService.ConvertCurency(currencyRate);
        }
    }

    //Merkez bankasından implemet edilen servis. Dokunamıyoruz..
    class CentralBankService
    {
        public decimal ConvertCurency(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)5.28;
        }
    }

    class IsBankServiceAdapter : IBankService
    {
        public decimal ConvertRate(CurrencyRate currencyRate)
        {
            IsBankService isBankService = new IsBankService();
            return isBankService.ConvertCurencyIsBank(currencyRate);
        }
    }

    class IsBankService
    {
        public decimal ConvertCurencyIsBank(CurrencyRate currencyRate)
        {
            return currencyRate.Price / (decimal)5.20;
        }
    }
}
