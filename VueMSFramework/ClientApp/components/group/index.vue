<template>
    <ae-dynamic-page :page="page"
                     :startUp="startUp"
                     :viewModelClassName="viewModelClassName"
                     access="private"
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
                page: "group",
                startUp: { section: "search", action: "search/load" },
                viewModelClassName: "VueMSFramework.ViewModels.Group.Index",
            }
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
<style>
</style>

<style scoped>
    .section{
        min-height:450px;
    }


</style>
