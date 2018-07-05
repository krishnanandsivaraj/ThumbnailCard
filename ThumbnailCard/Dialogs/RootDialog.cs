using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ThumbnailCardDemo.Dialogs
{
  [Serializable]
  public class RootDialog : IDialog<object>
  {
    public Task StartAsync(IDialogContext context)
    {
      context.Wait(MessageReceivedAsync);

      return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
    {
      var message = await result;
      var welcomeMessage = context.MakeMessage();
      welcomeMessage.Text = "Welcome to bot Thumbnail Card Demo";

      await context.PostAsync(welcomeMessage);

      await this.DisplayThumbnailCardDemo(context);
    }

    public async Task DisplayThumbnailCardDemo(IDialogContext context)
    {

      var replyMessage = context.MakeMessage();
      Attachment attachment = GetProfileThumbnailCardDemo(); ;
      replyMessage.Attachments = new List<Attachment> { attachment };
      await context.PostAsync(replyMessage);
    }

    private static Attachment GetProfileThumbnailCardDemo()
    {
      var ThumbnailCardDemo = new ThumbnailCard
      {
        // title of the card  
        Title = "Krishnanand Sivaraj",
        //subtitle of the card  
        Subtitle = "A normal guy",
        // navigate to page , while tab on card  
        Tap = new CardAction(ActionTypes.OpenUrl, "get Blog", value: "helperclass.wordpress.com"),
        //Detail Text  
        Text = "A normal guy's quest for software craftsmanship",
        // smallThumbnailCardDemo  Image  
        Images = new List<CardImage> { new CardImage("https://avatars1.githubusercontent.com/u/9303047?s=400&v=4") },
        
      };

      return ThumbnailCardDemo.ToAttachment();
    }
  }
}
