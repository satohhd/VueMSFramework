<template>
    <div v-if="_auth" :id="page" class="page" v-show="show">
        <ae-loading></ae-loading>
        <ae-dynamic-section :page="page" section="index" :viewModelClassName="viewModelClassName" @setBreadcrumb="setBreadcrumb" @teller="listener"></ae-dynamic-section>
        <ae-footer></ae-footer>
    </div>
</template>

<script>

    /* *****************************************************************
    *
    * ae-dynamic-page
    *
    * in string  page
    * in string  startUp
    * in string  viewModelClassName
    * in string  access               public/private
    *
    * emit teller
    *
    ******************************************************************* */

    import aeTransition from "./ae-transition-utils.js"
    export default {
        mixins: [aeTransition],
        props: [
            'page',
            'startUp',
            'viewModelClassName',
            'access',
        ],
        /** *****************************************************************
        *
        * data
        *
        ******************************************************************* */
       data() {
            return {
                focusField: null,
                show: true,
            }
        },
        watch: {
           /* ******************************************************************
            *
            * watch $route
            * Description  Monitor changes in URL.
            *
            ******************************************************************* */
            $route: function (to, from) {
                if (to && this.startUp.section && this.page) {
                    if (to.path == "/" + this.page) {
                        if (Object.keys(to.query).length == 0) {
                            this.invoke(this.startUp,{ _caller: 'watch' });

                       }
                    }
                }
            },
        },
        methods: {
            /* ******************************************************************
            *
            * setBreadcrumb
            *
            * param  []               [{text,to}]
            * return void
            *
            ******************************************************************* */
            setBreadcrumb: function (param) {
                if (!param) param = []
                param.unshift({ text: 'home', to: '/' });
                this.$store.dispatch('setBreadcrumb', param);
            },
        },
        /** *****************************************************************
        *
        * computed
        *
        * _auth
        * _applying
        *
        ******************************************************************* */
        computed: {
            _auth: function () {
                return this.$store.state.signInUser.authorized || (this.access == "public") || false;
            },
            _applying: function () {
                return this.$store.state.signInUser.applying ||  false;
            },
        },
        created: function () {
        },
        mounted: function () {
            this.loadingReset()

            //section all readyになるまでまつ
            setTimeout(function (self) {

                if (self._auth) {
                    if (Object.keys(self.$route.query).length == 0) {
                        console.log("mount no startUp " + JSON.stringify(self.startUp))
                        self.invoke(self.startUp);
                    }
                } else {
                    if (self._applying) {
                        alert('只今申請中のためこちらの機能はご利用できません。')
                        self.pageLoader("home")
                    } else {
                        //self.pageLoader("auth", "signIn", { redirect: { section: self.startUp.section, action: self.$route.fullPath } });
                        alert('SignInしてください')
                        self.pageLoader("home")
                  }
                }


            },400, this);



          //if (this._auth) {
          //      if (Object.keys(this.$route.query).length == 0) {
          //          this.invoke(this.startUp, { _caller: 'mounted' });
          //      }
          //  } else {
          //      if (this._applying) {
          //          alert('只今申請中のためこちらの機能はご利用できません。')
          //          this.pageLoader("home")
          //      } else {
          //          this.pageLoader("auth", "signIn", { redirect: { section: this.startUp.section, action: this.$route.fullPath } });
          //              //alert('SignInしてください')
          //     }
          //  }
        },
        destroyed: function () {
        }
    }
</script>
<style>
</style>
<style scoped>
</style>
