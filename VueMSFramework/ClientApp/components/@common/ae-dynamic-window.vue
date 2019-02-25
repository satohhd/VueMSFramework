<template>
    <div v-if="_show">
        <transition name="slide-fade">
            <b-alert show dismissible @dismissed="close" :class="'dialog ' + section"
                     :style="{
                        left: this._x,
                        top: this._y,
                        zIndex: this._zIndex,
                     }">

                <div class="header" @mousedown.prevent="mousedown">
                    <h1><span v-html="title" /></h1>
                </div>
                <hr />
                <ae-dynamic-section-form :page="page" :section="section" :title="title"  :fields="fields" :viewModel="viewModel" @teller="listener" @setBreadcrumb="setBreadcrumb"></ae-dynamic-section-form>
            </b-alert>
        </transition>
    </div>
</template>
<script>
    /* ******************************************************************
    *
    * ae-dialog
    *
    * param  string       viewModelClassName
    * param  string       page
    * param  string       section
    * param  string       title
    *
    ******************************************************************* */
    import aeTransition from "./ae-transition-utils.js"
    export default {
        mixins: [aeTransition],
        props: [
            'viewModelClassName',
            'page',
            'section',
            'title',
        ],
        data() {
            return {
                loading :false,
                fields: {},
                viewModel: {},
                initViewModel: { },
                focusField: null,
                meta: {},
                //show: true,
                 /* window control */
                wndID: 100,
                x: 0,
                y: 0,
                cursorOffset: { x: 0, y: 0 },
                cursorStartPos: null,
                stateAtSizeChangeStarted: { width: 0, height: 0, cursorX: 0, cursorY: 0 },
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
                            console.log("$route2")
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
            "_show": function (to, from) {
                if (to) {
                    this.postAndLoadSection();
                } else {
                    this.disposeSection();
                }
            }
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

                //Client preprocessing
                this.loadingOn()
                this.setBreadcrumb();
                var $current = null;
                if (document.activeElement) {
                    $current = document.activeElement.id;
                }

                //To server processing
                this.$http.post('/api/' + this.page + '/' + this.section, this.viewModel)
                    .then(response => {

                        //Client processing
                        this.$set(this, "viewModel", response.data)
                        if ($current) {
                            this.focusField = $current
                        }
                    })
                    .catch(error => {

                        //Error handling
                        console.log(this.page + ":" + error);
                        if (error.response.data) {
                            alert(error.response.data._message)
                        } else {
                            alert("error:" + error)
                        }
                        this.$router.replace("/")
                    })
                    .finally(() => {

                        //Post processing
                        this.loadingOff()
                    })

            },
 
            /** *****************************************************************
            *
            * closeSection
            *
            * param  void
            * return void
            *
            ******************************************************************* */
            close: function () {
                this.sectionCloser({ section: this.section })
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
                this.viewModel = Object.create(this.initViewModel)
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

            /** *****************************************************************
            *
            * Change position
            *
            ******************************************************************* */
            mousedown: function (e) {
                this.cursorOffset.x = e.pageX;
                this.cursorOffset.y = e.pageY;
                this.cursorStartPos = { x: this.x, y: this.y };
                document.addEventListener("mousemove", this.mousemove)
                document.addEventListener("mouseup", this.mouseup)
                this.$store.dispatch('moveWndToTop', { wndID: this.wndID });
            },
            mousemove: function (e) {
                this.x = this.cursorStartPos.x + (e.pageX - this.cursorOffset.x);
                this.y = this.cursorStartPos.y + (e.pageY - this.cursorOffset.y);
            },
            mouseup: function (e) {
                this.cursorStartPos = null;
                document.removeEventListener("mousemove", this.mousemove)
                document.removeEventListener("mouseup", this.mouseup)
            },
            enter: function () {
                this.setInitialState();
            },
            setInitialState: function () {
                //初期化が済んでいれば処理を終了
                if ((this.x !== null) && (this.y !== null)) return;
                if (this.initialPosition && this.initialPosition.length === 2) {
                    this.x = this.initialPosition[0];
                    this.y = this.initialPosition[1];
                } else {
                    this.x = (window.innerWidth / 2) - (this.$el.clientWidth / 2);
                    this.y = (window.innerHeight / 2) - (this.$el.clientHeight / 2);
                }
            },
            startSizeChange: function (e) {
                let wndRect = this.$el.getBoundingClientRect()
                this.stateAtSizeChangeStarted = {
                    width: wndRect.width,
                    height: wndRect.height,
                    cursorX: e.pageX,
                    cursorY: e.pageY
                };
                document.addEventListener('mousemove', this.whileSizeChange, false);
                document.addEventListener('mouseup', this.endSizeChange, false);
            },
            whileSizeChange: function (e) {
                this.width = this.stateAtSizeChangeStarted.width + e.pageX - this.stateAtSizeChangeStarted.cursorX
                this.height = this.stateAtSizeChangeStarted.height + e.pageY - this.stateAtSizeChangeStarted.cursorY
            },
            endSizeChange: function (e) {
                document.removeEventListener('mousemove', this.whileSizeChange, false);
                document.removeEventListener('mouseup', this.endSizeChange, false);
            },
        },
        computed: {
            _show: function () {
                return this.$route.query[this.section] != undefined
            },
            _width: function () {
                return this.width ? `${this.width}px` : 'auto';
            },
            _height: function () {
                return this.height ? `${this.height}px` : 'auto';
            },
            _x: function () {
                return `${this.x}px`;
            },
            _y: function () {
                return `${this.y}px`;
            },
            _zIndex: function () {
                return this.$store.state.wndStatuses[this.wndID].zIndex || 0;
            }
        },
        created: function () {
            this.createSection();

            this.wndID = this.$store.state.wndCount;
            this.$store.dispatch('setWndStatuses', { wndID: this.$store.state.wndCount });

        },
        mounted: function () {

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

        }
    }
</script>
<style scoped>
</style>
