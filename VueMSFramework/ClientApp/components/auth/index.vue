<template>
    <ae-dynamic-page :page="page"
                     :startUp="startUp"
                     :viewModelClassName="viewModelClassName"
                     access="public"
                     @teller="listener" />
</template>

<script>
    import aeTransition from "../@common/ae-transition-utils.js"
    import listener from "./listener.js"
    export default {
        mixins: [aeTransition, listener],
        /** *****************************************************************
        *
        * data
        *
        ******************************************************************* */
        data() {
            return {
            }
        },
        methods: {
            /** *****************************************************************
            *
            * initialize
            *
            ******************************************************************* */
            initialize: function () {
                console.log("initialize")
            },
        },
        created: function () {
            if (this._auth) {
                this.startUp = { section: "refer", action: "refer/load" }
            } else {
                this.startUp = { section: "signIn", action: "signIn" }
            }
        },
        mounted: function () {
            this.initialize();
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
                return this.$store.state.signInUser.authorized || false;
            },
            _applying: function () {
                return this.$store.state.signInUser.applying || false;
            },
        },
    }
</script>
<style lang='scss'>
</style>
<style scoped >
</style>
