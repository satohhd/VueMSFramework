<template>
    <div class="d-inline">
        <template v-for="f in field.childFields">
                <component

                            :page="page"
                            :section="section"
                            :field="f"
                            :viewModel="viewModel[field.name]"
                            :state="state" :is="'ae-tag-' + f.tag"
                            @setBreadcrumb="setBreadcrumb"
                            @teller="listener"></component>
        </template>
    </div>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {
            /* ******************************************************************
             *
             * listener
             *
             * param  string           section
             * param  {}               field{trigger}
             * param  {}               viewModel{..}
             * $emit teller
             *
             ******************************************************************* */
            listener: function (section, field, param) {

                if (this.section === section) {
                    this.$emit('teller', section, field, this.viewModel)

                } else {
                    this.$emit('teller', section, field, param)

                }
            },

        },
        mounted: function () {
        },
 
    }

</script>
<style scoped>
</style>
