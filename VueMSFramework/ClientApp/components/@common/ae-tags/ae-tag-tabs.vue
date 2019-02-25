<template>
    <b-tabs v-if="field.childFields" :id="_id">
        <template v-for="tab in field.childFields">
            <b-tab v-if="tab.tag=='tab'"  :key="tab.name"
                    :title="tab.caption"
                    :active="viewModel[field.name]['activeTabName']===tab.name"
                    @click="viewModel[field.name]['activeTabName']=tab.name">
                <ae-dynamic-section-form :page="page"
                                         :section="section"
                                         :fields="tab.childFields"
                                         :viewModel="(viewModel[field.name])[tab.name]"
                                         @teller="listener"></ae-dynamic-section-form>
            </b-tab>
        </template>
    </b-tabs>
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
            'page',
            'section',
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

        },

    }

</script>
<style scoped>
</style>
