using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_ApplicationStephen.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Call<UserProfile>(new EnsureProfileDialog(), ProfileEnsured);
            return Task.CompletedTask;
            //PromptDialog.Text(context, NameEntered, @"Hi! What is your name?");
            ////PromptDialog.Text(context, AgeEntered, @"What age are you?");
            //return Task.CompletedTask;

            

            //await context.PostAsync(@"Hello Bots! ");
            //context.Wait(MessageReceivedAsync);

            //var activity = await result as Activity;

            //// calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            //// return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            //context.Wait(MessageReceivedAsync);
        }

        private async Task ProfileEnsured(IDialogContext context, IAwaitable<UserProfile> result)
        {
            var profile = await result;

            context.UserData.SetValue(@"profile", profile);

            await context.PostAsync($@"Hello {profile.Name}, I love {profile.Company}");

            context.Wait(MessageReceivedAsync);
        }

        //private async Task AgeEntered(IDialogContext context, IAwaitable<string> result)
        //{
        //    await context.PostAsync($@"You are {await result}");
        //    context.Wait(MessageReceivedAsync);
        //}

        //private async Task NameEntered(IDialogContext context, IAwaitable<string> result)
        //{
        //    await context.PostAsync($@"Hi {await result}.  My name is Pat.");
        //    context.Wait(MessageReceivedAsync);
        //}
    }
}