using Telegram.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Telegram.Bot.Args;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace eeegyr
{
    public static class HandlerMessage
    {
        public static TelegramBotClient botClient =
            new TelegramBotClient("768225568:AAECzTM9t6SIVMe19oS9FaDX2_nsI8-9lL8");

        public static List<long> userId = new List<long>();

        public static void SendMessage()
        {
            //botClient.OnMessage += Bot_OnMessags;
            //botClient.OnCallbackQuery += Bot_OnMessage;
            //botClient.OnCallbackQuery += Bot_OnMessage1;
            //botClient.OnCallbackQuery += Bot_OnMessage2;
            botClient.StartReceiving();
            botClient.OnMessage += BotOnMessageReceived;
            //Thread.Sleep(int.MaxValue);
        }

        public static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (userId.Contains(message.Chat.Id) == false)
            {
                userId.Add(message.Chat.Id);
                if (message?.Type == MessageType.Text)
                {
                    await HandlerMessage.Test();
                }
            }
        }


        public static async Task Test()
        {
            // botClient.StartReceiving();
            var firstMsg = await botClient.AwaitMessage();
            var chatId = firstMsg.Chat.Id;
            var state = KeyBoardWork.KeeBoardForTest();
            var fortext = state.CurrentStep.Text;
            Message message = null;
            var flag = true;
            while (flag)
            {

                try
                {
                    var keyBoard = KeyBoardWork.KeeBoardForTest2(state.CurrentStep);
                    if (message != null)
                    {
                        if (state.CurrentStep.Text == fortext)
                        {

                            await botClient.EditMessageReplyMarkupAsync(chatId, message.MessageId, keyBoard);
                        }
                        else
                        {
                            await botClient.EditMessageTextAsync(chatId, message.MessageId, state.CurrentStep.Text);
                            await botClient.EditMessageReplyMarkupAsync(chatId, message.MessageId, keyBoard);
                            fortext = state.CurrentStep.Text;
                        }

                        //await botClient.EditMessageTextAsync(chatId,message.MessageId,state.CurrentStep.Text);
                    }
                    else
                    {
                        Console.WriteLine("pol");
                        message = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: state.CurrentStep.Text,
                            parseMode: ParseMode.Markdown,
                            disableNotification: true,
                            replyMarkup: keyBoard
                        );
                    }

                    var q = await botClient.AwaitCallback(chatId);
                    if (q.Data == "\U0001F51C")
                    {
                        var currentStepIdx = state.Steps.FindIndex(x => x == state.CurrentStep);
                        state.CurrentStep = state.Steps[(currentStepIdx + 1) % state.Steps.Count];
                        Console.WriteLine(state.CurrentStep.Text);
                    }


                    else
                    {
                        if (q.Data == " \U0001F51C ")
                        {
                            if (new ParceMessage().GetStude(state).Length == 0)
                            {
                                await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "студия не найдена ",
                                    parseMode: ParseMode.Default,
                                    disableNotification: true
                                );
                                flag = false;
                            }

                            ParceMessage text = new ParceMessage();
                            var res = text.GetStude(state);
                            Console.WriteLine(res.Length);
                            await botClient.SendTextMessageAsync(
                                chatId: chatId,
                                text:
                                "Готово! Переходи по ссылкам и изучай подробнее. \nРады были стараться! Заглядывай еще. Если у тебя остались вопросы, или ты хочешь заказать фотосессию, то знай, где нас искать\n  INSTA: @fotoland_ph\n VK: https://vk.com/fotoland_ph\n FB: https://www.facebook.com/fotolandph/\n" +
                                res,
                                parseMode: ParseMode.Default,
                                disableNotification: true
                            );
                            flag = false;
                        }
                        else
                        {
                            var currentStepIdx = state.CurrentStep.Buttons.First(x => x.Text == q.Data);
                            currentStepIdx.Flag = !currentStepIdx.Flag;
                        }
                    }

                    await botClient.AnswerCallbackQueryAsync(q.Id);
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }
            }

            HandlerMessage.Test();
        }




        static async void Bot_OnMessage(object sender, CallbackQueryEventArgs ev)
        {
            var e = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "Select to place")
            {
                var rkm = KeyBoardWork.KeeBoardForPlace();
                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Chat,
                    text: "Select the price that suits you",
                    parseMode: ParseMode.Default,
                    disableNotification: true,
                    replyMarkup: rkm);
            }
        }

        static async void Bot_OnMessage1(object sender, CallbackQueryEventArgs ev)
        {
            var e = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "Select to type")
            {
                var rkm = KeyBoardWork.KeeBoardForType();
                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Chat,
                    text: "Select the place that suits you",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: rkm);
            }
        }

        static async void Bot_OnMessage2(object sender, CallbackQueryEventArgs ev)
        {
            var e = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "Select to style")
            {
                var rkm = KeyBoardWork.KeeBoardForStyle();
                Message message = await botClient.SendTextMessageAsync(
                    chatId: e.Chat,
                    text: "Select the place that suits you",
                    parseMode: ParseMode.Markdown,
                    disableNotification: true,
                    replyMarkup: rkm);
            }
        }
    }
}
