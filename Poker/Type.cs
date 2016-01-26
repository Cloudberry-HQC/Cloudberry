namespace Poker
{
    /// <summary>
    /// Class that holds the points and hand factor.
    /// </summary>
    public class Type
    {   
        /// <summary>
        /// Points which are calculated according to the current hand.
        /// </summary>
        public double Power { get; set; }
        
        /// <summary>
        /// Hand factor according to the current hand.
        /// </summary>
        public double HandFactor { get; set; }
    }
}