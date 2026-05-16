namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card
{
    /// <summary>
    ///     Stores all available cards.
    /// </summary>
    public interface ICardLibrary
    {
        /// <summary>
        ///     Gets a card from a given code.
        /// </summary>
        /// <param name="code">Card code.</param>
        /// <returns>Card data for the code if it exists, empty data otherwise.</returns>
        ICardData GetCard(uint code);
        bool HasCard(uint code);
    }
}