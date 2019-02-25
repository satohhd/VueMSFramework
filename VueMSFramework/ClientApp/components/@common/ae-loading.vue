<template>
    <div v-if="_loading" class="loader">
        Loading...
    </div>
</template>
<script>
    /* ******************************************************************
    *
    * ae-loading
    *
    * store   int         $store.state.loading
    * param  void
    * return void
    *
    ******************************************************************* */
    export default {
        computed: {
            "_loading": function () {
                return this.$store.state.loading > 0;
            },
        },
    }

</script>

<style scoped>
    .loader,
    .loader:after {
        border-radius: 50%;
        width: 10em;
        height: 10em;
    }

    .loader {
        z-index: 1000;
        position: absolute;
        top: 0px;
        right: 0px;
        bottom: 0px;
        left: 0px;
        width: 10em;
        height: 10em;

        margin: auto;
        font-size: 10px;
        /*position: relative;*/
        text-indent: -9999em;
        border-top: 1.1em solid rgba(192,192,192, 0.2);
        border-right: 1.1em solid rgba(192,192,192, 0.2);
        border-bottom: 1.1em solid rgba(192,192,192, 0.2);
        border-left: 1.1em solid #c0c0c0;
        -webkit-transform: translateZ(0);
        -ms-transform: translateZ(0);
        transform: translateZ(0);
        -webkit-animation: load8 1.1s infinite linear;
        animation: load8 1.1s infinite linear;
    }

    @-webkit-keyframes load8 {
        0% {
            -webkit-transform: rotate(0deg);
            transform: rotate(0deg);
        }

        100% {
            -webkit-transform: rotate(360deg);
            transform: rotate(360deg);
        }
    }

    @keyframes load8 {
        0% {
            -webkit-transform: rotate(0deg);
            transform: rotate(0deg);
        }

        100% {
            -webkit-transform: rotate(360deg);
            transform: rotate(360deg);
        }
    }

</style>
