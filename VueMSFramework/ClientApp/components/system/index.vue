<template>
    <ae-dynamic-page :page="page"
                     :startUp="startUp"
                     :viewModelClassName="viewModelClassName"
                     access="public"
                     @teller="listener" />
</template>

<script>

    import aeTransition from "../@common/ae-transition-utils.js"
    export default {
        mixins: [aeTransition],


        /** *****************************************************************
        *
        * ローカルのデータ
        *
        ******************************************************************* */


        data() {
            return {
                page: "system",
                startUp: { section: "setting", action: "setting/load" },
                viewModelClassName: "VueMSFramework.ViewModels.System.Index",
            }
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
                return true;
            },
            _applying: function () {
                return this.$store.state.signInUser.applying || false;
            },
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

                    default:
                        this.invoke(field.event, param, field.redirect);
                        break;
                }

            },
        },
    }
</script>

<style scoped>
</style>
