namespace DO
{
    public struct OrderItem
    {
        public int OrderItemId { get; set; } //ID number for an item on the order
        public int ProductId { get; set; } //ID number for a product
        public int OrderId { get; set; } //ID number for an order
        public float Price { get; set; } //Total price of the item in the order
        public int Amount { get; set; } //The amount of items in the order
        /// <summary>
        /// A function to print an item in an order
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $@"
         Order item ID: {OrderItemId} 
         product ID: {ProductId}
         order ID: {OrderId}
         Price: {Price}
         Amount in stock: {Amount}";
    }
}
