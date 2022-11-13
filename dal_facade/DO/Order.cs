namespace DO
{
    public struct Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public override string ToString() => $@"
       Order ID: {OrderId}
       Customer Name: {CustomerName}
       Customer Adress: {CustomerAdress}
       Customer Email: {CustomerEmail}";
    }
}