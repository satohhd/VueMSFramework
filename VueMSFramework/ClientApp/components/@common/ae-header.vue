<template>
    <header class="header">
        <ae-menu :page="page" @teller="listener"></ae-menu>
        <b-breadcrumb v-if="_breadcrumb" :items="_breadcrumb" />
    </header>
</template>

<script>
    /* ******************************************************************
    *
    * ae-header
    *
    * in  string       page
    * in  string       title
    *
    ******************************************************************* */

    export default {

        props: ["page","title"],

        /** *****************************************************************
        *
        * ローカルのデータ
        *
        ******************************************************************* */
        data() {
            return {
                subtitle: "Make the system more familiar! Conveniently!",
                //breadcrumbItems: [{ text: 'home', to: '/'}, { text: this.title, to: this.page } ]
            }
        },

        methods: {
            /** *****************************************************************
            *
            * アクション、パラメータオブジェクト、遷移先ページオブジェクト
            *
            ******************************************************************* */
            listener: function (section,field, param) {
                this.$emit('listener', field, param)
            },

        },
        computed: {
            _breadcrumb: function () {
                return this.$store.state.breadcrumb;
            },
        },
    }
</script>

<style lang="scss">
    .header {
        .breadcrumb
        {
            font-size: 0.8em;
        }
        .breadcrumb a {
            color: #a0a0a0;
        }
        .breadcrumb a[href="/"]
        {
            background: url(/images/menu/home.png) center no-repeat;
            background-size: contain;
            width: 30px;
            height: 30px;
            text-indent: -9999px;
            text-decoration: none;
            color: transparent;
        }
        .breadcrumb-item + .breadcrumb-item::before {
            content: "/";
        }
    }

</style>
