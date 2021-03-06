//WCF Service
public decimal GetTotalForOrder(Order newOrder, RunTimeClass runtimeValue)
{   
    IExchangeRate ex = new ExchangeRate(runtimeValue);
    IOrderCalculator calculator = ObjectFactory.With&lt;IExchangeRate&gt;(ex).GetInstance<IOrderCalculator>();
    return calculator.CalculateOrderTotal(newOrder);    
}
