﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;
using MonoTouch.Twitter;

[assembly:ExportRenderer(typeof(CodeCamp.TweetButton), typeof(CodeCamp.TweetButtonRenderer))]

namespace CodeCamp
{
	public class TweetButtonRenderer : ButtonRenderer
	{

		protected override void OnElementChanged (ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged (e);
		
			var tweetButton = e.NewElement as TweetButton;

			var button = Control as UIButton;

			button.TouchUpInside += (object sender, EventArgs ea) => {
				var tweetController = new TWTweetComposeViewController();
				tweetController.SetInitialText (tweetButton.Tweet); 

				var parentview = button.Superview;
				parentview.Window.RootViewController.PresentModalViewController(tweetController, true);
			};
		}
	}
}

