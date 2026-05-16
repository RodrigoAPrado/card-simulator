using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Card;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel.Enum;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Duel
{
    /// <summary>
    ///     Ocg Duel Instance. Wraps a duel from the ocgcore lib.
    /// </summary>
    public interface IDuel
    {
        /// <summary>
        ///     Current duel state.
        /// </summary>
        DuelState State { get; }

        /// <summary>
        ///     List of the latest messages received from the ocgcore lib.
        /// </summary>
        IReadOnlyList<IDuelMessage> Messages { get; }

        /// <summary>
        ///     Current message index. Use this to track which message is being currently being accessed within the duel.
        /// </summary>
        int CurrentMessageIndex { get; }

        /// <summary>
        ///     Current amount of retries. Goes up whenever an invalid response is set to the ocgcore lib.
        /// </summary>
        int RetryCount { get; }

        /// <summary>
        ///     Gets the ocgcore lib version being used.
        /// </summary>
        /// <returns>Major, minor</returns>
        Tuple<int, int> GetOcgVersion();

        /// <summary>
        ///     Sets up the duel's options.
        /// </summary>
        /// <param name="duelMode"></param>
        /// <param name="player1Lp"></param>
        /// <param name="player1Hand"></param>
        /// <param name="player1Draw"></param>
        /// <param name="player2Lp"></param>
        /// <param name="player2Hand"></param>
        /// <param name="player2Draw"></param>
        /// <returns></returns>
        bool SetupDuelOptions(
            DuelMode duelMode,
            uint player1Lp,
            uint player1Hand,
            uint player1Draw,
            uint player2Lp,
            uint player2Hand,
            uint player2Draw
        );

        /// <summary>
        ///     Initiates the ocg duel.
        /// </summary>
        /// <returns>True if , false otherwise</returns>
        bool InitDuel();

        bool SetDecks(
            IReadOnlyList<ICardData> mainDeck0,
            IReadOnlyList<ICardData> extraDeck0,
            IReadOnlyList<ICardData> mainDeck1,
            IReadOnlyList<ICardData> extraDeck1
        );

        bool StartDuel();

        bool ProceedDuel();

        bool SetResponse(List<int> playerInput);

        bool SetCancel();

        bool NextMessage();
    }
}