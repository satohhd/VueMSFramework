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
        * data
        *
        ******************************************************************* */
        data() {
            return {
                page: "msection",
                startUp: { section: "index", action: "index" },
                viewModelClassName: "VueMSFramework.ViewModels.MSection.Index",
           }
        },
        methods: {
            /** *****************************************************************
            *
            * init
            *
            ******************************************************************* */
            init: function () {

            },
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
        mounted: function () {
            this.init();
        },

    }
</script>
<style lang='scss'>
</style>
<style scoped >
</style>
