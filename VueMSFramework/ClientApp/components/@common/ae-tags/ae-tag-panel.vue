<template>
    <div class="d-inline" :id="_id">
        <template v-for="f in field.child.fields">
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
        },
        mounted: function () {
        },
 
    }

</script>
<style scoped>
</style>
