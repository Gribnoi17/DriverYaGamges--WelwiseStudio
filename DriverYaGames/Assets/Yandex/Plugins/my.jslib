mergeInto(LibraryManager.library, {

  	Hello: function () {
    	window.alert("Hello, world!");
    	console.log("Hello, world!");
  	},

	GiveMePlayerData: function () {
    	myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    	myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
  	},

  	RateGame: function () {
    
    	ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  	},

  detectDeviceUser: function () { 
		const ua = navigator.userAgent;
		if (/(tablet|ipad|playbook|silk)|(android(?!.*mobi))/i.test(ua)) {
			return 1;
		}
		if (
			/Mobile|iP(hone|od)|Android|BlackBerry|IEMobile|Kindle|Silk-Accelerated|(hpw|web)OS|Opera M(obi|ini)/.test(
			ua
			)
		) {
			return 1;
		}
		return 3;
	},


	SaveExtern: function(date) {
    	var dateString = UTF8ToString(date);
    	var myobj = JSON.parse(dateString);
    	player.setData(myobj);
  	},

  	LoadExtern: function(){
    	player.getData().then(_date => {
        	const myJSON = JSON.stringify(_date);
        	myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
    	});
 	},

 	SetToLeaderboard : function(value){
    	ysdk.getLeaderboards()
      	.then(lb => {
        lb.setLeaderboardScore('Milage', value);
      });
  	},

  GetLang: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
    },

    ShowAdv : function(){
        ysdk.adv.showFullscreenAdv({
          callbacks: {
          onClose: function(wasShown) {
            console.log("------------- closed --------------");
            // some action after close
        	},
          onError: function(error) {
            // some action on error
        	}
        }
        })
    },

    ReturnToGameExtern : function(){
        ysdk.adv.showRewardedVideo({
          callbacks: {
          onOpen: () => {
            console.log('Video ad open.');
        },
          onRewarded: () => {
            console.log('Rewarded!');
        },
          onClose: () => {
            console.log('Video ad closed.');
            myGameInstance.SendMessage("LosePanelActivation", "ReturnToGame");
        }, 
          onError: (e) => {
            console.log('Error while open video ad:', e);
        }
        }
        })
    },


  });