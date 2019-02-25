export default {
    /** *****************************************************************
    *
    * setting
    *
    ******************************************************************* */
    data() {
        return {
            page: "auth",
            startUp: { section: "index", action: "index" },
            viewModelClassName: "VueMSFramework.ViewModels.Auth.Index",
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
        listener: function (section, field, param) {
            switch (field.event.action) {
                case "back":
                    this.$router.go(-1)
                    break;

                case "index":
                    //console.log(param)
                    //console.log({ authorized: param.authorized, applying: param.applying, userName: param.userName })
                    this.$store.dispatch('setSignInUser', { authorized: param.authorized, applying: param.applying, userName: param.userName });
                    localStorage.setItem('signInUser', JSON.stringify(param))
                    this.invoke(field.event, param, field.redirect);
                    break;

                case "signOut":
                    localStorage.clear()
                    this.$store.dispatch('setSignInUser', {});
                    this.invoke(field.event, param);
                    break;
                default:
                    this.invoke(field.event, param, field.redirect);
                    break;
            }
        },
    }
}
