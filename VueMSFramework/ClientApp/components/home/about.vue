<template>
    <div :class="page">
        <header class="header">
            <h1 class="title">
            </h1>
            <ae-menu :page="page"></ae-menu>
        </header>
        <section class="content">
            <p>This site is created with a framework constructed using microsoft company's technology such as Vue and dotnet.Core 2.2, MVC, Linq etc. as a concept of multi section.</p>
        </section>
        <ae-footer></ae-footer>
    </div>
</template>

<script>
    export default {

        /** *****************************************************************
        *
        * ローカルのデータ
        *
        ******************************************************************* */

        data() {
            return {
                page: "about",
                title: "Experimental site",
                subtitle: "Make the system more familiar! Conveniently!",
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
            initPage: function (pageName, param) {
            },
        },
        computed: {

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

        },
        /** *****************************************************************
        *
        * インスタンスが破棄される前
        *
        ******************************************************************* */
        beforeDestroy: function () {
        },
        /** *****************************************************************
        *
        * インスタンスが破棄される後
        *
        ******************************************************************* */
        destroyed: function () {
        }
    }
</script>

<style scoped>
    .header {
        background-color: white;
    }
    .content {
        min-height: 50vh;
        margin: 0 15px;
    }

</style>
