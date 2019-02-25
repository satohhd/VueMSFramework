<template>
    <section v-if="_show" :id="section" class="section">
        <ae-header v-if="section === 'index'" :page="page" :title="meta.caption" @teller="listener"></ae-header>
        <ae-dynamic-section-form :page="page" :section="section" :title="meta.caption" :fields="fields" :viewModel="viewModel" @setBreadcrumb="setBreadcrumb" @teller="listener" v-show="show">
        </ae-dynamic-section-form>
    </section>
</template>

<script>
    /* ******************************************************************
    *
    * ae-section
    *
    * in  string    page
    * in  string    section
    * in  string    title
    * in  string    viewModelClassName
    *
    ******************************************************************* */
    import aeTransition from "./ae-transition-utils.js"
    export default {
        mixins: [aeTransition],
        props: [
            'page',
            'section',
            'title',
            'viewModelClassName',
        ],
        /** *****************************************************************
        *
        * data
        *
        ******************************************************************* */
        data() {
            return {
                show:true,
                fields: {},
                viewModel: {},
                initViewModel: {},
                focusField: null,
                loading: false,
                meta: {},
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

                console.log("$route")
                if (this.section == "index") {
                    this.setBreadcrumb();
                    return;
                }
                if (this.section == null) {
                    if (Object.keys(to.query).length == 0) {
                        //console.log("$route")
                        this.postAndLoadSection();
                    }
                } else {
                    //When the value of the parameter is changed
                    if (to.query[this.section] !== undefined && from.query[this.section] !== undefined) {
                        if (to.query[this.section] !== from.query[this.section]) {
                            this.postAndLoadSection();
                        }
                    } else {
                        //if (to.query[this.section] !== undefined) {
                        //    console.log("$route3")
                        //   this.postAndLoadSection();
                        //}
                    }

                }
            },
           /* ******************************************************************
            *
            * watch _show
            * Description  Show or hide sections
            *
            ******************************************************************* */
            "_show": function (to, from) {
                if (to) {
                    console.log("_show:" + this.section)
                    this.postAndLoadSection();
                } else {
                    console.log("_hide:" + this.section)
                    this.disposeSection();
                }
            },
            /* ******************************************************************
            *
            * watch focusField
            * Description
            *
            ******************************************************************* */
            "focusField": function (e, old) {
                if (e) {
                    if (e == undefined || e == null) return;
                    if (e == old) return;
                    if (document.getElementById(e)) {
                        setTimeout(function (id) {
                            document.getElementById(e).focus();
                        }, 200, e);
                    }
                }
            },

        },
        methods: {
            /* ******************************************************************
            *
            * createSection
            *
            * param  void
            * return void
            *
            ******************************************************************* */
            createSection: function () {
                console.log("createSection : " + this.section)
                 //Client preprocessing
                if (!this.viewModelClassName) return;
                this.loadingOn();


                //To server processing
                this.$http.get('/api/' + this.page + '/dna/' + this.viewModelClassName)
                    .then(response => {

                        //Client processing
                        this.viewModel = Object.create(response.data.viewModel);
                        this.initViewModel = Object.create(response.data.viewModel);
                        this.fields = response.data.fields;
                        this.meta = response.data.meta;

                    })
                    .catch(error => {

                        //Error handling
                        console.log(this.page + ":" + error);
                        alert("error:" + error)
                        this.$router.replace("/")
                    })
                    .finally(() => {
                        //Post processing
                        this.loadingOff()
                    })
            },
            /* ******************************************************************
            *
            * postAndLoadSection
            *
            * param  void
            * return void
            *
            ******************************************************************* */
            postAndLoadSection: function () {
                console.log("postAndLoadSection")
                
                //Client preprocessing
                this.loadingOn()
                this.setBreadcrumb();
                var $current = null;
                if (document.activeElement) {
                    $current = document.activeElement.id;
                }

                //Retrieve a value from a parameter
                var issuer = 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxx';
                var authUserStr = localStorage.getItem('signInUser');
                if (authUserStr) {
                    var signInUser = JSON.parse(authUserStr);
                    if (signInUser) {
                        if (signInUser.accountId) {
                            issuer = signInUser.accountId;
                        }
                    }
                }


                var param = this.$jwt.decode(this.$route.query[this.section], issuer);
                if (param === null) param = {}
                for (let key in param) {
                    console.log(key + "=" + param[key])
                    if (typeof this.viewModel[key] !== 'undefined') {
                       this.$set(this.viewModel,key,param[key])
                    }
                }

                //URL
                var objParam = {}
                var query = this.$route.query;
                for (let s in query) {
                    if (s == this.section) {
                        objParam[s] = query[s]
                    }
                }

                //SingleSection
                this.$router.replace({ path: this.page, query: objParam })

                //To server processing
                var action = this.section;
                if (param['_event']) {
                    action = param._event.action;
                }
                console.log('/api/' + this.page + '/' + action)
                let copyObj = {};
                Object.assign(copyObj, this.viewModel);
                //console.log(copyObj)

                this.$http.post('/api/' + this.page + '/' + action, copyObj)
                    .then(response => {
                        //Client processing
                        if (response.data._redirect && response.data._redirect.action) {
                            this.$emit('teller', this.section, { event: response.data._redirect }, response.data)
                            //this.invoke(response.data._redirect, response.data)
                        } else {
                           this.$set(this, "viewModel", response.data)
                           if ($current) {
                                this.focusField = $current
                            }
                        }

                    })
                    .catch(error => {

                        //Error handling
                        console.log(this.page + ":" + error);
                        if (error.response && error.response.data && error.response.data._message ) {
                            alert(error.response.data._message)
                        } else {
                            alert("error:" + error)
                        }
                        

                        if (error.response.data._errorRedirect && error.response.data._errorRedirect.action) {
                            this.invoke(error.response.data._errorRedirect, error.response.data)
                        }
                        //this.$router.replace("/")
                    })
                    .finally(() => {

                        //Post processing
                        this.loadingOff()
                    })

            },
            /* ******************************************************************
            *
            * disposeSection
            *
            * param  void
            * return void
            *
            ******************************************************************* */
            disposeSection: function () {
                //this.$set(this, "viewModel", Object.assign({},this.initViewModel))
                this.viewModel = Object.create(this.initViewModel)
                //this.viewModel = this.initViewModel
                //this.viewModel = Object.assign({}, this.initViewModel)
            },
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
                if (this.section === "index") {
                    param.unshift({ text: this.meta.caption, to: this.page });
                } else {
                    param.unshift({ text: this.meta.caption, to: this.section });
                }
                this.$emit('setBreadcrumb', param)
            },
        },
        computed: {
            _show: function () {
                return (this.section == "index") || (this.$route.query[this.section] != undefined) || (this.section == null && Object.keys(this.$route.query).length == 0);
                //return (this.section == "index") || (this.$route.query[this.section] != undefined) ;
            },
        },
        created: function () {
            this.loadingReset()
            this.wndID = this.$store.state.wndCount;
            this.$store.dispatch('setWndStatuses', { wndID: this.$store.state.wndCount });
            this.createSection();
        },
        mounted: function () {
            console.log("mounted=" + this.section)
            this.$emit('require-inner-item', el => {
                this.$refs.wndInner.appendChild(el);
                //（v-show=falseの時は要素の高さが取れないので初期化しない）
                if (this.visible && this.$el) {
                    this.setInitialState();

                }
            });

            if (this.$route.query[this.section]) {
                setTimeout(function (self) {
                    self.postAndLoadSection();
                }, 400, this);
            }

        },
    }

</script>
<style scoped>
</style>
