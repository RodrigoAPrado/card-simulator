using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel
{
    /// <summary>
    ///     Gives access to cards and manages duels.
    /// </summary>
    public interface IDuelManager
    {
        /// <summary>
        ///     All cards available
        /// </summary>
        ICardLibrary CardLibrary { get; }

        /// <summary>
        ///     The ongoing duel. Null if no duel is ongoing.
        /// </summary>
        IDuel CurrentDuel { get; }

        /// <summary>
        ///     Creates an OcgDuel and sets it as Current Duel.
        ///     If there is already an ongoing OcgDuel, it does nothing.
        /// </summary>
        /// <returns>True if created, false otherwise.</returns>
        bool CreateDuel();

        /// <summary>
        ///     Disposes of the Current Duel.
        ///     If there is no ongoing OcgDuel, it does nothing.
        /// </summary>
        /// <returns>True if disposed, false otherwise.</returns>
        bool DisposeDuel();
    }
}