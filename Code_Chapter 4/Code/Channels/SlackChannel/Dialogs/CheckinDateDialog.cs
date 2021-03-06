﻿namespace SlackChannel.Dialogs
{
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Bot.Builder.Dialogs.IDialog{System.Int32}" />
    [Serializable]
    public class CheckinDateDialog : IDialog<DateTime>
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckinDateDialog"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CheckinDateDialog(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// The start of the code that represents the conversational dialog.
        /// </summary>
        /// <param name="context">The dialog context.</param>
        /// <returns>
        /// A task that represents the dialog start.
        /// </returns>
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"{ this.name }, what is your checkin date?");

            context.Wait(this.MessageReceivedAsync);
        }

        /// <summary>
        /// Messages the received asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            DateTime checkinDate;

            if (DateTime.TryParse(message.Text, out checkinDate) && (checkinDate > DateTime.UtcNow))
            {
                context.Done(checkinDate);
            }
        }
    }
}