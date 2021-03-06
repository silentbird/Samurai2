Welcome to Flurry AppCircle!

This README contains:

1. Introduction
2. AppCircle Integration
3. Optional Features
4. Banner-less Integration
5. Callbacks
6. FAQ

=====================================
1. Introduction

Flurry AppCircle is an application recommendation network. Publishers can integrate AppCircle into their applications to provide new application recommendations to their users and earn commissions on sales they generate. 
Promoters can use the AppCircle network of publishers to promote their applications to new users. 
 
These instructions describe how to become a Publisher by adding AppCircle recommendations to your application. 
It is designed to be as easy as possible and since it uses the same SDK as Flurry Analytics, you can get set up in under 5 minutes.

Note that you don't have to use AppCircle in order to enjoy Flurry Analytics. However, you do need to have Flurry Analytics integrated into your application to enjoy the benefits of AppCircle.

=====================================
2. AppCircle Integration

When you integrate AppCircle into your application, a banner will be placed over your application's view to display recommendations. Clicking on the banner will display a canvas that gives more information about the application recommended in the banner. 
The user never leaves your application while browsing recommendations, so a user can cancel out of the canvas and go back to the previous application view.

First, enable AppCircle before starting the session:

[FlurryAPI setAppCircleEnabled:YES];
[FlurryAPI startSession:@"YOUR_API_KEY"];

Adding AppCircle hooks is as easy as adding a single call to getHook in any view controller where you want to add a promotion. 
The only code you need to add is the following: 

UIView *banner = [FlurryAPI getHook:@"PUT_YOUR_HOOK_NAME_HERE" xLoc:0 yLoc:0 view:self.view];

If you don't use a view controller, feel free to use another view that you display in the application.

Note that this should be done after you've created your view. When this view is loaded the AppCircle system will promote other applications and pay you for any installations generated by those promotions. 

That is all that is required to add basic AppCircle recommendations to your application. You're done!

=====================================
3. Optional Features

You can pass in any UIView as the banner's parent view, here's an example using the key window as the parent view:

UIView *banner = [FlurryAPI getHook:@"PUT_YOUR_HOOK_NAME_HERE" xLoc:0 yLoc:0 view:self.view];

If you require more control on the banner itself, use the longer getHook method with optional parameters:

UIView *banner = [FlurryAPI getHook:@"USE_YOUR_OWN_HOOK_NAME" xLoc:0 yLoc:0 view:self.view attachToView:YES orientation:@"portrait" canvasOrientation:@"portrait" autoRefresh:YES canvasAnimated:YES];

- attachToView controls whether the banner is automatically placed on the parent view. The default setting is YES.
- orientation controls the length of the banner. The values are @"portrait" and @"landscape". @"portrait" sets the banner dimension at 320x48. @"landscape" sets the banner dimension at 480x48. The default setting is @"portrait".
- canvasOrientation controls the canvas orientation. The values are @"portrait", @"landscapeRight", and @"landscapeLeft". The default setting is @"portrait".
- autoRefresh controls whether the banner will automatically update itself with new ads after a given period provided from the Flurry server. The current refresh interval is 20 seconds. The default setting is NO.
- canvasAnimated controls whether the canvas will animate when it displays and closes. The default setting is YES.

After requesting the banner with getHook method, subsequent calls with the same parent and hook will result in the same banner instance being returned. Only one banner will be created per hook and parent view. We recommend updating existing banners with new ads instead of creating new banners.

To update an existing banner with new ad:

[FlurryAPI updateHook:banner];

To remove an existing banner from its parent view and hook in order to create additional banners on the same hook and parent view:

[FlurryAPI removeHook:banner];
[banner removeFromSuperview];

=====================================
4. Banner-less Integration

While the AppCircle recommendation banner is a popular option due to it's standard size and shape, some developers prefer to integrate AppCircle into their application menu or other custom interface elements. 
To satisfy this need, you can launch the AppCircle recommendation interface without using the banner display. 

If you want to display a canvas page without displaying any banner, call this method anywhere in your application:

[FlurryAPI openCatalog:@"USE_YOUR_OWN_HOOK_NAME" canvasOrientation:@"portrait" canvasAnimated:YES];

A new view will open on top of your current view and display recommended applications. The user will be able to go back to whatever view launched the interface easily by clicking the "Back" button. 

=====================================
5. Callbacks

You can pass in a delegate to FlurryAPI in order to receive callbacks from FlurryAPI. This is an optional feature that allows more control over the how your application interacts with AppCircle.
The following example shows how it can be used with the application delegate. You can pass in any object as the delegate.

First, include the FlurryAdDelegate.h file from the Flurry SDK (FlurryLib or FlurryLibWithLocation) that you are using into your project.

In your AppDelegate.h:
// add FlurryAdDelegate.h file to the project if you are using FlurryAdDelegate
#import "FlurryAdDelegate.h"

// add FlurryAdDelegate as a protocol
@interface AppDelegate : NSObject <UIApplicationDelegate, FlurryAdDelegate> {
}

In your AppDelegate.m:
// implement to do something when the data is available
// currently just output debug message
- (void)dataAvailable {
    NSLog(@"Flurry data is available");
}
// implement to do something when the data is unavailable
// currently just output debug message
- (void)dataUnavailable {
    NSLog(@"Flurry data is unavailable");
}
// implement to do something when the canvas will open
// currently just output debug message
- (void)canvasWillDisplay:(NSString *)hook {
    NSLog(@"Flurry canvas will display:%@", hook);
}
// implement to do something when the canvas will close
// currently just output debug message
- (void)canvasWillClose {
    NSLog(@"Flurry canvas will close");
}
// set FlurryAdDelegate before the session starts
- (void)applicationDidFinishLaunching:(UIApplication *)application {
    [FlurryAPI setAppCircleDelegate:self];
    [FlurryAPI startSession:@"YOUR_API_KEY"];
}

=====================================
6. FAQ

Do you have to use AppCircle?

If you want to just use the analytics tool in Flurry Agent, you can turn off AppCircle with an optional setting: [FlurryAPI setAppCircleEnabled:NO];

=====================================

Please let us know if you have any questions. If you need any help, just email iphonesupport@flurry.com!

Cheers,
The Flurry Team
http://www.flurry.com
iphonesupport@flurry.com