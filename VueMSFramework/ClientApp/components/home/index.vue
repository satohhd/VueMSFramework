<template>
    <transition name="fade">
        <div :class="page">
            <header class="header">
                <h1 class="title">
                    <span v-html="title"></span>
                    <span class="subtitle" v-html="subtitle"></span>
                </h1>
                    <b-carousel id="carousel1"
                                style="text-shadow: 1px 1px 2px #333;"
                                controls
                                indicators
                                background="#ababab"
                                :interval="50000"
                                img-width="1024"
                                img-height="480"
                                v-model="slide"
                                @sliding-start="onSlideStart"
                                @sliding-end="onSlideEnd">

                        <!-- Text slides with image -->
                        <b-carousel-slide caption=""
                                          text="We are developing systems in the environment of lush green Sinshyu."
                                          img-src="/images/slide1.png">
                        </b-carousel-slide>
                        <b-carousel-slide caption=""
                                          text="When coming from Nagano city, cross the Ochiai bridge where the Chikumagawa and Saikawa meet, you can see Taro Wako Taro, a symbol."
                                          img-src="/images/slide3.png">
                        </b-carousel-slide>
                    </b-carousel>
                    <ae-menu page="home"></ae-menu>
            </header>
            <section class="content">
                <b-card-group columns>
                   <b-card title="Welcome! aeBrain">
                       <p class="card-text">
                           Experimental site using Vue.<br />This site is created with a framework constructed using microsoft company's technology such as Vue and dotnet.Core 2.2, MVC, Linq etc. as a concept of multi section.
                       </p>
                    </b-card>
                </b-card-group>
            </section>
            <ae-footer></ae-footer>
        </div>
    </transition>
</template>

<script>
    import aeTransition from "../@common/ae-transition-utils.js"
    export default {
        mixins: [aeTransition],
        /** *****************************************************************
        *
        * data
        *
        ******************************************************************* */
        data() {
            return {
                page: "home",
                title: "VueMultiSectionFramework alpha",
                subtitle: "Make the system more familiar! Conveniently!",
                fields: {},
                viewModel: {},
                show: {
                    activity: false,
                    product: false,
                    contact: false,
                    volunteer: false,
                    labo: false,
                    about: false,
                },
                slide: 0,
                sliding: null,

            }
        },

        watch: {

            /** *****************************************************************
            *
            * ルートの変更監視
            *
            ******************************************************************* */
            $route: function (to, from) {
            },
        },
        methods: {
            /** *****************************************************************
            *
            * initPage
            *
            ******************************************************************* */
            initPage: function () {
                //this.$store.dispatch('setAuthorize', { authorize: this.isAuth });
            },
         

            /** *****************************************************************
            *
            * Slide
            *
            ******************************************************************* */
            onSlideStart(slide) {
                this.sliding = true
            },
            onSlideEnd(slide) {
                this.sliding = false
            },
            onDismissed: function (index) {
                this.dismissCountDown[index] = 0
            },
            countDownChanged(dismissCountDown) {
                this.dismissCountDown = dismissCountDown;
            },
            onDismissed: function (index) {
                this.dismissCountDown[index] = 0
            },

     

        },
        /** *****************************************************************
        *
        * サーバーと接続する
        *
        ******************************************************************* */
        created: function () {

       },
        /** *****************************************************************
        *
        * サーバーから受信する部分
        *
        ******************************************************************* */
        mounted: function () {
            this.initPage();
        },
        /** *****************************************************************
        *
        * インスタンスが破棄される前
        *
        ******************************************************************* */
        beforeDestroy: function () {
            //this.connection.stop();
        },
        /** *****************************************************************
        *
        * インスタンスが破棄される後
        *
        ******************************************************************* */
        destroyed: function () {
        },
        computed: {
        },

    }
</script>
<style lang="scss">


</style>
<style scoped>
    .header {
        height: auto;
    }
    .grid {
        display: grid;
        grid-gap: 10px;
        /*grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));*/
        grid-template-columns: repeat(auto-fit, minmax(620px, 1fr));
        justify-content: center;
    }
    .header {
        background-color: white;
    }
    .content {
        min-height: 50vh;
        margin: 10px 15px;
    }
 
</style>
