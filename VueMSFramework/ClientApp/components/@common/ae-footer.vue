<template>
    <footer class="bd-footer text-muted">
        <div class="container">
            <p>
                Copyright (C) 2018 ae Brain. All Rights Reserved.
            </p>
        </div>
    </footer>
</template>

<script>
    /* ******************************************************************
    *
    * ae-footer
    *
    * param  string       page
    *
    ******************************************************************* */
    export default {

        props: ["page"],

        /** *****************************************************************
        *
        * ローカルのデータ
        *
        ******************************************************************* */
        data() {
            return {

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

        }
    }
</script>

<style scoped>

    .container {
        text-align: center;
    }


</style>
