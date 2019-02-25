export default {
        data: function () {
            return {
                uiTagPrefix:"ae-tag-",
            }
        },
        methods: {
 
            /* ******************************************************************
             *
             * listener
             *
             * param  string           section
             * param  {}               field{event}
             * param  {}               viewModel{..}
             * return void
             *
             ******************************************************************* */
            listener: function (section: string, field: {}, param: {}) {
                this.$emit('teller',section, field, param)
            },
            /** *****************************************************************
            *
            * loadingReset
            *
            * param  void 
            * return void
            *
            ******************************************************************* */
            loadingReset: function () {
                this.$store.dispatch('resetLoading');
            },
            /** *****************************************************************
            *
            * loadingOn
            *
            * param  void 
            * return void
            *
            ******************************************************************* */
            loadingOn: function () {
                this.$store.dispatch('addLoading',1);
            },
            /** *****************************************************************
            *
            * loadingOFF
            *
            * param  void 
            * return void
            *
            ******************************************************************* */
            loadingOff: function () {
                this.$store.dispatch('addLoading', -1);
            },
            ///** *****************************************************************
            // *
            // * pageDispatcher
            // *
            // * param  string        page
            // *
            // ******************************************************************* */
            //pageDispatcher: function (page: string, section: string, event: string, param: {}) {
            //     this.$router.push({ path: page })
            //},

            /** *****************************************************************
            *
            * invoke
            *
            * event {}            {page,section,action,paramItems}
            * param  {}            viewModel
            * return void
            *
            ******************************************************************* */
            invoke: function (event: {}, param: {}, redirect: {}, redirectParam: {}) {
                console.log("invoke start")
                if (event["section"] === null) {
                    console.log("invoke param missing")
                    return;
                }
                
                if (!param) param = {}
                var param2 = {};

                if (event['paramItems']) {
                    let ary = event['paramItems'].split(',');
                    for (let s in ary) {
                        let key = ary[s]
                        param2[key] = param[key]
                    }
                }
                if (event) {
                    param2['_event'] = event;
                }
                if (redirect) {
                    param2['_redirect'] = redirect;
                }
           　　 if (redirectParam) {
                    param2['_redirectParam'] = JSON.stringify(redirectParam);
                }
             　 if (event["page"] === null) {
                    event["page"] = this.page;
                }
   
                var objParam = {}
                var query = this.$route.query;
                for (let s in query) {
                    objParam[s] = query[s]
                }


                var token = this._createToken(param2);
                if (!token) {
                    alert('token error')
                } else {
                    objParam[event["section"]] = token
                    this.$router.push({ path: event["page"], query: objParam })
                }

            },

            /** *****************************************************************
            *
            * sectionCloser
            *
            * event {}            {url,section,page}
            * return bool          void
            *
            ******************************************************************* */
            sectionCloser: function (event: {}) {

                var objParam = {}

                if (event["page"] === null) {
                    event["page"] = this.page;
                }
                var query = this.$route.query;
                for (let s in query) {
                    if (s != event["section"])
                        objParam[s] = query[s]
                }
                this.$router.replace({ path: event["page"], query: objParam })
            },
  
            /** *****************************************************************
            *
            * fileDownloader
            *
            * param  string        section
            * param  {}            viewModel
            * return bool          true/false
            *
            ******************************************************************* */
            fileDownloader: function (id: string) {

                this.$http.get('/api/' + this.page + '/export/' + id)
                    .then(response => {
                        this._downloadFile(response.data.fileContents, response.data.contentType, response.data.fileDownloadName)
                    })
                    .catch(error => {
                        alert(error)
                    })
            },

 
            /** *****************************************************************
            *
            * _createToken
            * param {}      {email}
            * return        string/null
            *
            ******************************************************************* */
            _createToken: function (param: {}) {

                if (param == undefined) param = {}
                param["timestamp"] = Math.floor(Date.now() / 5000)

                //var issuer = this.$store.state.signInUser === undefined ? "Anonymous" : this.$store.state.signInUser.userName
                //チケットの一部を暗号化の文字とする先頭５－２５ →今はaccountId
                var issuer = 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxx';
                var authUserStr = localStorage.getItem('signInUser');
                //var authUserStr = localStorage.getItem(param.accountId);
                if (authUserStr) {
                    var signInUser = JSON.parse(authUserStr);
                    if (signInUser) {
                        if (signInUser.accountId) {
                            //issuer = signInUser.ticket.substring(5, 25);
                            issuer = signInUser.accountId;
                        }
                    }
                }
                //キーあり
                if (issuer) {
                    var secret = issuer;
                    var token = this.$jwt.sign(param, secret, { algorithm: 'HS256', issuer: issuer });

                    //var token = encodeURIComponent(JSON.stringify(param))

                    return token;

                } else {
                    //this.pageLoader("auth")
                    return null;
                }

            },

            ///* ******************************************************************
            // *
            // * signOut
            // *
            // * param  {}               viewModel{..}
            // * return void
            // *
            // ******************************************************************* */
            //signOut: function () {
            //    console.log("signOut")

            //    this.loadingOn();

            //    //ユーザ情報
            //    var signInUser = { ticket: null }
            //    var authUserJson = localStorage.getItem('signInUser')
            //    if (authUserJson) {
            //        signInUser = JSON.parse(authUserJson);
            //    }


            //    //サインアウト
            //    this.$http.post("/api/auth/signedOut", signInUser)
            //        .then(response => {

            //           // localStorage.removeItem(response.data.email + "@" + response.data.termAddr)
            //           // localStorage.removeItem('signInUser');

            //        })
            //        .catch(error => {

            //            if (error.response.data._message) {
            //                alert(error.response.data._message)
            //            } else {
            //                alert(error)
            //            }
            //        })
            //        .finally(() => {
            //            //ストレージ、変数のクリア
            //            localStorage.clear()
            //            this.$store.dispatch('setSignInUser', {});

            //            this.loadingOff()
            //            this.pageLoader("home");
            //        });
            //},

            ///* ******************************************************************
            // *
            // * signIn
            // *
            // * param  {}            viewModel {email}
            // * return void
            // *
            // ******************************************************************* */
            //signIn: function (param) {
            //    if (!param) return;
            //    //if (!param.email) {
            //    //    alert("E-mail address is not entered.")
            //    //    return;
            //    //}
            //    //if (!param.termAddr) {
            //    //    alert("Can not get the terminal address.")
            //    //    return;
            //    //}
            //    localStorage.removeItem('signInUser');
            //    this.$store.dispatch('setSignInUser', {});

            //    //サーバー認証前に、ローカルの認証リストをチェックする
            //    var authUserJson = localStorage.getItem(param.email + "@" + param.termAddr)
            //    if (!authUserJson) {


            //        //
            //        //alert("未登録です。アカウント登録してからSignInしてください。")
            //        //this.getIPs(function (ip, self) {
            //        //    self.pageLoader("auth", "create", { termAddr: ip, email: param.email });
            //        //})
            //        //return;
            //        //param.ticket = signInUser.ticket


            //    } else {

            //        var signInUser = JSON.parse(authUserJson)
            //        param.accountId = signInUser.accountId
            //        param.email = signInUser.email
            //        param.termAddr = signInUser.termAddr
            //        param.remoteAddr = signInUser.remoteAddr
            //        param.ticket = signInUser.ticket
            //    }
            //    //サインイン
            //    //param  { email: param.email }
            //    param.event = "signIn"
            //    this.$http.post("/api/auth/signIn", param)
            //        .then(response => {
            //            //認証されたかどうか
            //            if (response.data.authorized) {
            //                this.$store.dispatch('setSignInUser', { authorized: response.data.authorized, applying: response.data.applying, userName: response.data.userName });
            //                //現在利用しているユーザ signInUser{}
            //                localStorage.setItem('signInUser', JSON.stringify(response.data))
            //                //認証済みユーザリスト   param.email
            //                localStorage.setItem(response.data.email + "@" + response.data.termAddr, JSON.stringify(response.data))

            //                if (response.data.redirect) {
            //                    this.$router.replace(response.data.redirect)
            //                } else {
            //                    this.pageLoader("auth", "refer");
            //                }


            //            } else {
            //                //申請処理中です
            //                if (response.data.applying) {
            //                    alert("只今申請処理中です")
            //                } else {
            //                    alert("不明")
            //                }
            //            }

            //        })
            //        .catch(error => {
            //            if (error.response.data._message) {

            //                //申請処理中の確認
            //                if (error.response.data.applying) {

            //                    // 「OK」時の処理開始 ＋ 確認ダイアログの表示
            //                    if (window.confirm('本人確認のメール送信済みですが、再申請しますか？')) {
            //                        this.pageLoader("auth", "create", { email: param.email });
            //                    } else {
            //                        this.pageLoader("home");
            //                    }
            //                    return;
            //                }
            //                // 「キャンセル」時の処理終了

            //                alert(error.response.data._message)

            //                param.Authorized = false;
            //                param.Applying = false;

            //                //本人確認中
            //                if (error.response.data.applying) {
            //                    this.pageLoader("home");
            //                } else if (!error.response.data.authorized) {

            //                    //alert("未登録です。アカウント登録してからSignInしてください。")
            //                    this.pageLoader("auth", "create", { email: param.email });
            //                    //this.getIPs(function (ip, self) {
            //                    //    self.pageLoader("auth", "create", { termAddr: ip, email: param.email });
            //                    //})
            //                    return;
            //                } else {
            //                    this.pageLoader("home");
            //                }


            //            } else {
            //                alert(error)
            //            }
            //        })
            //        .finally(() => this.loadingOff());


            //},


            /* ******************************************************************
             *
             * _downloadFile
             *
             * param  string           base64
             * param  string           mime_ctype
             * param  string           filename
             * return void
             *
             ******************************************************************* */
            _downloadFile: function (base64, mime_ctype, filename) {
                var blob = this.toBlob(base64, mime_ctype, false);

                if (window.navigator.msSaveBlob) {
                    // IEやEdgeの場合、Blob URL Schemeへと変換しなくともダウンロードできる
                    window.navigator.msSaveOrOpenBlob(blob, filename);
                } else {
                    // BlobをBlob URL Schemeへ変換してリンクタグへ埋め込む
                    const url = window.URL.createObjectURL(blob);
                    var link = document.createElement('a');
                    link.href = url;
                    link.download = filename;
                    link.click();
                }
            },
            /* ******************************************************************
             *
             * toBlob 
             * Base64とMIMEコンテンツタイプからBlobオブジェクトを作成する。
             * param base64
             * param mime_ctype MIMEコンテンツタイプ
             * param bom  true:bom ari false: bom nasi
             * returns Blob
             *
             ******************************************************************* */
            toBlob: function (base64, mime_ctype, isBom) {
                // 日本語の文字化けに対処するためBOMを作成する。
                var bom = new Uint8Array([0xEF, 0xBB, 0xBF]);

                var bin = atob(base64.replace(/^.*,/, ''));
                var buffer = new Uint8Array(bin.length);
                for (var i = 0; i < bin.length; i++) {
                    buffer[i] = bin.charCodeAt(i);
                }
                // Blobを作成
                try {
                    if (isBom) {
                        var blob = new Blob([bom, buffer.buffer], { type: mime_ctype, });
                    } else {
                        var blob = new Blob([buffer.buffer], { type: mime_ctype, });
                    }
                } catch (e) {
                    return false;
                }
                return blob;
            }

        }
    }
