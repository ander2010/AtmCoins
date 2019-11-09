using System;
using Xunit;
using AtmCoins;

namespace AtmCoinsTest
{
    public class ProgramTest
    {

       

        [Fact]
        public void Withdraw_SelectionFirstBillWithDif_Selection()
        {
            // arrange
            int diff = 10;

            // act
            int billValue=Program.Selecction(diff);

            // assert
            Assert.Equal(10, billValue);
        }



        [Fact]
        public void Withdraw_ObtainAmountofBillsFromKey_PrintBill()
        {
            // arrange
            string keyFactor = "100";

            // act
            string valueFactor = Program.PrintBill(keyFactor);

            // assert
            Assert.Equal("10", valueFactor);
        }



        [Fact]
        public void Withdraw_ObtainNoresponseFromDictionary_PrintBillThrow()
        {
            // arrange
            string keyFactor = "105";

            // act
            //string valueFactor = Program.PrintBill(keyFactor);

            // act // assert
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(() => Program.PrintBill(keyFactor));
        }

        
    }
}

