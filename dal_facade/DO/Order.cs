namespace DO
{
    public struct Order
    {
        public int OrderId { get; set; } //ID number of the order
        public string CustomerName { get; set; } //customer name
        public string CustomerAdress { get; set; } //Customer's residential address
        public string CustomerEmail { get; set; } //Customer email address
        public DateTime? OrderDate { get; set; }  //Date the order was placed
        public DateTime? ShipDate { get; set; } //Date the order was sent
        public DateTime? DeliveryDate { get; set; } //The date of receipt of the order
        /// <summary>
        /// Function to print order details
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $@"
       Order ID: {OrderId}
       Customer Name: {CustomerName}
       Customer Adress: {CustomerAdress}
       Customer Email: {CustomerEmail}
       Order Date: {OrderDate}
       Ship Date: {ShipDate}
       Delivery Date: {DeliveryDate}";
    }
}