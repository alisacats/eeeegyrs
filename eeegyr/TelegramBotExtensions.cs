using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace eeegyr
{
    public static class TelegramBotExtensions
    {
        public static async Task<CallbackQuery> AwaitCallback(this TelegramBotClient tg, long chatId)
        {
            var tcs = new TaskCompletionSource<CallbackQuery>();
            void EventHandler(object obj, CallbackQueryEventArgs args)
            {
                var q = args.CallbackQuery;
                if (q.Message.Chat.Id == chatId) tcs.SetResult(q);
            }

            tg.OnCallbackQuery += EventHandler;
            var res = await tcs.Task;
            tg.OnCallbackQuery -= EventHandler;
            return res;
        }

        public static async Task<Message> AwaitMessage(this TelegramBotClient tg)
        {
            var tcs = new TaskCompletionSource<Message>();
            void EventHandler(object obj, MessageEventArgs args)
            {
                tcs.SetResult(args.Message);
            }

            tg.OnMessage += EventHandler;
            var res = await tcs.Task;
            tg.OnMessage -= EventHandler;
            return res;
        }
    }
}
