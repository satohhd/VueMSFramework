export default {
    data: function () {
        return {
        }
    },
    created: function () {
        this.$store.dispatch('setSignInUser', {});
        var signInUserJson = localStorage.getItem('signInUser');
        if (signInUserJson ) {
            const signInUser = JSON.parse(signInUserJson);

            if (signInUser) {
                this.$store.dispatch('setSignInUser', { authorized: signInUser.authorized, applying: signInUser.applying, userName: signInUser.userName });
                //this.$store.dispatch('setSignInUser', { authorized: response.data.authorized, applying: response.data.applying, userName: response.data.userName });

            }

        }
        var lang = localStorage.getItem('lang');
        if (lang) {
            this.$store.dispatch('setLang', lang);
        }


        // Add a request interceptor
        this.$http.interceptors.request.use((config) => {
            const signInUser = JSON.parse(localStorage.getItem('signInUser'));
            if (!signInUser) {
                if (config.headers) {
                    if (config.headers["Authorization"]) {
                        alert('delete')
                        delete config.headers["Authorization"];
                    }
                }
                //config.headers['Authorization'] = 'Bearer ' + this.signInUser.token
            }
            else {
                if (signInUser.ticket) {
                    //alert('auth1 = ' + signInUser.token)
                    config.headers['Authorization'] = 'Bearer ' + signInUser.ticket
                    //console.log('Bearer  ' + signInUser.ticket)
                } else {
                    if (config.headers) {
                        if (config.headers["Authorization"]) {
                            alert('delete2')
                            delete config.headers["Authorization"];


                        }
                    }
                }

                //言語設定がある場合
                const lang = localStorage.getItem('lang');
                if (lang) {
                    config.headers['accept-language'] = lang
                }
          }

            //  config.headers['X-Requested-With'] = 'XMLHttpRequest'
            //     config.headers['Expires'] = '-1'
            //     config.headers['Cache-Control'] = 'no-cache,no-store,must-revalidate,max-age=-1,private'
            //// Do something before request is sent
            //this.loading = true
            return config
        }, (error) => {
            console.log('request failed')
            this.loading = false
            // Do something with request error
            alert(error)
            return;
            //return Promise.reject(error)
        })

        

        //// Add a response interceptor
        //this.$http.interceptors.response.use((response) => {

        //    //  alert('response')
        //    // Do something with response data
        //    return response
        //}, (error) => {
        //    console.log('response failed')
        //    // Do something with response error
        //    this.loading = false
        //    return Promise.reject(error)
        //})


    },
}
